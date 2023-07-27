"""
********************************************************************************************

Service methods for events

********************************************************************************************
"""

from __future__ import annotations
from dataclasses import dataclass
from uuid import UUID
import flask
import requests
from tasks.domain.models.api_responses import EventApiResponse
from tasks.common.api_url_builder import ApiUrlBuilder
from tasks.common import security
from tasks.common.structs import BaseReturn

@dataclass
class GetEventApiResponse(BaseReturn):
    data: str = None


@dataclass
class GetEventModelResult(BaseReturn):
    data: EventApiResponse | None = None


#------------------------------------------------------
# Send an api request to update an event using the data gathered from the request form
#------------------------------------------------------
def update_event_from_request(event_id: UUID) -> requests.Response:
    return send_put_request(event_id, flask.request.form)

#------------------------------------------------------
# Send a PUT request to the api for the specified event using the given event data
#------------------------------------------------------
def send_put_request(event_id, event_data) -> requests.Response:
    response = requests.put(
        verify = False,
        auth   = security.get_user_session_tuple(),
        url    = _build_api_event_url(event_id),
        data   = event_data,
    )

    return response

#------------------------------------------------------
# Get an event from the api
#------------------------------------------------------
def get_event_model(event_id: UUID) -> GetEventModelResult:
    # fetch the data from the api
    api_response = send_get_request(event_id)

    if not api_response.successful:
        return api_response

    result = GetEventModelResult(successful=True)

    try:
        # parse the json string into a dict
        json_object = flask.json.loads(api_response.data)

        # serialize the dict into a domain model
        result.data = EventApiResponse.from_dict(json_object)
    
    except Exception as ex:
        result.successful = False
        result.error = ex

    return result


#------------------------------------------------------
# Get the specified event from the api
#------------------------------------------------------
def send_get_request(event_id: UUID) -> GetEventApiResponse:
    result = GetEventApiResponse(successful=True)

    # url = _build_api_event_url(event_id)

    response = requests.get(
        verify = False,
        auth   = security.get_user_session_tuple(),
        url    = _build_api_event_url(event_id),
    )

    if not response.ok:
        result.successful = False
        result.error = requests.HTTPError(response)

    result.data = response.text

    return result



#------------------------------------------------------
# Delete the specified event
#------------------------------------------------------
def delete_event(event_id: UUID) -> BaseReturn:
    result = BaseReturn()

    try:
        api_response = _send_delete_request(event_id)
        result.data = api_response.text
        result.successful = api_response.ok

    except Exception as ex:
        result.successful = False
        result.error = ex

    return result


#------------------------------------------------------
# Send a delete api request
#------------------------------------------------------
def _send_delete_request(event_id) -> requests.Response:
    api_response = requests.delete(
        url    = _build_api_event_url(event_id),
        auth   = security.get_user_session_tuple(),
        verify = False,
    )

    return api_response


#------------------------------------------------------
# Build the url for the /events resource for the api
#------------------------------------------------------
def _build_api_event_url(event_id) -> str:
    url_builder = ApiUrlBuilder()
    return url_builder.events(event_id)

    
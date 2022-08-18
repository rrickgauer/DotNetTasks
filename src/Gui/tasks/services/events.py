from __future__ import annotations
from datetime import date
from uuid import UUID
import flask
import requests
from tasks.config.routines import get_config
from tasks.common import security
from tasks.common.structs import BaseReturn

#------------------------------------------------------
# Send an api request to update an event using the data gathered from the request form
#------------------------------------------------------
def update_event_from_request(event_id: UUID) -> requests.Response:
    response = requests.put(
        verify = False,
        auth   = security.get_user_session_tuple(),
        url    = _build_api_event_url(event_id),
        data   = flask.request.form,
    )

    return response


#------------------------------------------------------
# Get the specified event from the api
#------------------------------------------------------
def get_event(event_id: UUID) -> BaseReturn:
    result = BaseReturn(successful=True)

    url = _build_api_event_url(event_id)

    response = requests.get(
        verify = False,
        auth   = security.get_user_session_tuple(),
        url    = url,
    )

    if not response.ok:
        result.successful = False
        result.error = requests.HTTPError(response)

    result.data = response.text

    return result

#------------------------------------------------------
# Build the url for the /events resource for the api
#------------------------------------------------------
def _build_api_event_url(event_id) -> str:
    config = get_config()
    url = f'{config.URL_API}/events/{event_id}'

    return url


def delete_event(event_id: UUID) -> BaseReturn:
    result = BaseReturn(successful=True)

    api_response = requests.delete(
        url    = _build_api_event_url(event_id),
        auth   = security.get_user_session_tuple(),
        verify = False,
    )

    return result


def delete_event_occurence(event_id: UUID, date_val: date):
    return 'delete_event_occurence'

def delete_event_occurence_following(event_id: UUID, date_val: date):
    return 'delete_event_occurence_following'

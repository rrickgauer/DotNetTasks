from __future__ import annotations
from uuid import UUID
import flask
import requests
from tasks.config.routines import get_config
from tasks.common import security

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
# Build the url for the /events resource for the api
#------------------------------------------------------
def _build_api_event_url(event_id) -> str:
    config = get_config()
    url = f'{config.URL_API}/events/{event_id}'

    return url
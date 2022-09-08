"""
********************************************************************************************

Service methods for event labels

********************************************************************************************
"""

from __future__ import annotations
from uuid import UUID
import requests
from tasks.common.api_url_builder import ApiUrlBuilder
from tasks.common import security
import flask


#------------------------------------------------------
# Get the labels assigned to the specified event
#------------------------------------------------------
def get_event_labels(event_id: UUID) -> requests.Response:
    url_builder = ApiUrlBuilder()
    url = url_builder.event_labels(event_id)

    response = requests.get(
        url    = url,
        auth   = security.get_user_session_tuple(),
        verify = False,
    )

    return response



def update_event_labels(event_id: UUID, labels: list[str]) -> requests.Response:

    url_builder = ApiUrlBuilder()
    url = url_builder.event_labels(event_id)

    response = requests.get(
        url    = url,
        auth   = security.get_user_session_tuple(),
        verify = False,
        data = flask.json.dumps(labels),
    )

    return response




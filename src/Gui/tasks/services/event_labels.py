"""
********************************************************************************************

Service methods for event labels

********************************************************************************************
"""

from __future__ import annotations
from uuid import UUID
import requests
from typing import List
from tasks.apiwrapper import ApiUrlBuilder
from tasks.domain.models import LabelResponse
from tasks.apiwrapper import ApiWrapperEventLabels
from tasks.common import security
import flask


def get_event_labels(event_id: UUID) -> List[LabelResponse]:
    """Get the labels assigned to the specified event"""

    # request labels from the api
    api = ApiWrapperEventLabels(event_id)
    response = api.get()

    # serialize the response data into domain models
    response_data = response.json()
    labels = LabelResponse.from_dicts(response_data)

    return labels




def update_event_labels(event_id: UUID, labels: list[str]) -> requests.Response:
    url_builder = ApiUrlBuilder()
    url = url_builder.event_labels(event_id)

    response = requests.put(
        url     = url,
        auth    = security.get_user_session_tuple(),
        verify  = False,
        headers = {'content-type': 'application/json'},
        data    = flask.json.dumps(labels),
    )

    return response




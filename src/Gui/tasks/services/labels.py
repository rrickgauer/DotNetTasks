"""
********************************************************************************************

Service methods for labels

********************************************************************************************
"""

from __future__ import annotations
import re
import flask
from dataclasses import dataclass
from uuid import UUID
import requests
from tasks.common import serializers
from tasks.domain.models.api_responses import LabelResponse
from tasks.common import security
from tasks.common.structs import BaseReturn
from tasks.common import ApiUrlBuilder

@dataclass
class GetLabelsResult(BaseReturn):
    data: list[LabelResponse] = None
    

#------------------------------------------------------
# Get the user's labels
#------------------------------------------------------
def get_labels() -> GetLabelsResult:
    result = GetLabelsResult(successful=True)

    # fetch the data from the api
    response = _send_get_request()

    if not response.ok:
        result.successful = False
        result.error = response.text

    # serialize the json objects into label models
    labels = response.json().get('data', [])
    
    result.data  = _serialize_labels(labels)

    return result



def _send_get_request() -> requests.Response:
    url_builder = ApiUrlBuilder()
    url = url_builder.labels()

    response = requests.get(
        url    = url,
        auth   = security.get_user_session_tuple(),
        verify = False,
    )

    return response


def _serialize_labels(labels: list[dict]) -> list[LabelResponse]:
    models = []

    for label in labels:
        models.append(_serialize_label(label))

    return models


def _serialize_label(label: dict) -> LabelResponse:
    serializer = serializers.LabelResponseSerializer(label)

    model = serializer.serialize()

    return model


def get_labels_html(labels: list[LabelResponse]) -> str:

    labels_macro = flask.get_template_attribute('macros/labels.html', 'get_labels_board')

    html = labels_macro(labels)

    return html



def get_label(label_id: UUID) -> requests.Response:
    url_builder = ApiUrlBuilder()
    url = url_builder.label(label_id)

    response = requests.get(
        url    = url,
        auth   = security.get_user_session_tuple(),
        verify = False,
    )

    return response


def update_label(label_id: UUID, data: dict) -> requests.Response:
    url_builder = ApiUrlBuilder()
    url = url_builder.label(label_id)

    response = requests.put(
        url    = url,
        auth   = security.get_user_session_tuple(),
        verify = False,
        data = data
    )

    return response


def delete_label(label_id: UUID) -> requests.Response:
    url_builder = ApiUrlBuilder()
    url = url_builder.label(label_id)

    response = requests.delete(
        url    = url,
        auth   = security.get_user_session_tuple(),
        verify = False,
    )

    return response
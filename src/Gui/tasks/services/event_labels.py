"""
********************************************************************************************

Service methods for event labels

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
from tasks.common import serializers


def get_event_labels(event_id: UUID):
    url_builder = ApiUrlBuilder()
    url = url_builder.event_labels(event_id)


    response = requests.get(
        url = url,
        auth = security.get_user_session_tuple(),
        verify=False,
    )

    return response





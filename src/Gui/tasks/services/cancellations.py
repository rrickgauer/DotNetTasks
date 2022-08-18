"""
********************************************************************************************

Service methods for event cancellations

********************************************************************************************
"""

from __future__ import annotations
from datetime import date
from uuid import UUID
import requests
from tasks.common import security
from tasks.common.structs import BaseReturn
from tasks.common import ApiUrlBuilder

#------------------------------------------------------
# Send a request to the api for a single event cancellation 
#------------------------------------------------------
def create_cancellation(event_id: UUID, day: date) -> BaseReturn:
    result = BaseReturn()

    try:
        response = _send_put_request(event_id, day)
        result.successful = response.ok
        result.data = response.text
    except Exception as ex:
        result.successful = False
        result.error = ex

    return result


#------------------------------------------------------
# Send a PUT request to the api
#------------------------------------------------------
def _send_put_request(event_id, day) -> requests.Response:
    url_builder = ApiUrlBuilder()

    response = requests.put(
        verify = False,
        auth   = security.get_user_session_tuple(),
        url    = url_builder.cancellations(event_id, day),
    )

    return response




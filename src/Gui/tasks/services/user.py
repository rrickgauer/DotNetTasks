from __future__ import annotations
from dataclasses import dataclass
from enum import Enum
import flask
from tasks.common.structs import BaseReturn
from tasks.domain import models
from tasks.common import security, serializers
import requests
from tasks.common import ApiUrlBuilder


#------------------------------------------------------
# Forward the incoming signup request to the api
#------------------------------------------------------
def send_signup_request():
    response = _send_signup_api_request()

    response_data = response.json()

    serializer = serializers.UserSignUpApiResponseSerializer(response_data)
    result = serializer.serialize()

    return result


def _send_signup_api_request() -> requests.Response:
    url_builder = ApiUrlBuilder()
    url = url_builder.user_sign_up()

    custom_header = security.get_custom_request_header()

    response = requests.post(
        url     = url,
        data    = flask.request.form,
        verify  = False,
        headers = custom_header,
    )

    return response



def request_user_info() -> requests.Response:
    url_builder = ApiUrlBuilder()
    url = url_builder.user()

    custom_header = security.get_custom_request_header()

    response = requests.get(
        url     = url,
        verify  = False,
        headers = custom_header,
        auth    = security.get_user_session_tuple(),
    )

    return response

@dataclass
class UpdateUserResponse(BaseReturn):
    data: requests.Response = None


def update_user(outgoing_data: dict) -> UpdateUserResponse:
    result = UpdateUserResponse(successful=True)
    
    try:
        # get the existing user data from the api
        result.data = request_user_info()

        if not result.data.ok:
            return result

        existing_user_data: dict = result.data.json()

        # copy over any missing info into the outgoing data before sending it to the api
        outgoing_data.setdefault('email', existing_user_data.get('email'))
        outgoing_data.setdefault('deliverReminders', existing_user_data.get('deliverReminders'))

        result.data = _send_update_request(outgoing_data)

    except Exception as ex:
        result.successful = False
        result.error = ex

    return result



def _send_update_request(user_data):
    url_builder = ApiUrlBuilder()
    url = url_builder.user()

    custom_header = security.get_custom_request_header()


    # data = u

    response = requests.put(
        url     = url,
        data    = user_data,
        verify  = False,
        headers = custom_header,
        auth    = security.get_user_session_tuple(),
    )

    return response


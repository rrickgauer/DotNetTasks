from __future__ import annotations
from enum import Enum
import flask
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


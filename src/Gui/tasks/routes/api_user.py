"""
********************************************************************************************

Url Prefix: /api/user

api endpoints

********************************************************************************************
"""

from __future__ import annotations
import flask
from tasks.common import security
from tasks import services
from http import HTTPStatus
import flask_responses

# module blueprint
bp_api_user = flask.Blueprint('api_user', __name__)

#------------------------------------------------------
# POST: /api/user/signup
#------------------------------------------------------
@bp_api_user.post('signup')
def sign_up():
    result = services.user.send_signup_request()

    if not result.successful:
        return flask_responses.bad_request(result)

    security.clear_session_values()
    security.set_session_values(result.user.email, result.user.password)

    return flask_responses.get()

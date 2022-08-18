"""
********************************************************************************************

Url Prefix: /api/login

api endpoints

********************************************************************************************
"""

from __future__ import annotations
import flask
from tasks import services
from http import HTTPStatus

# module blueprint
bp_api_login = flask.Blueprint('api_login', __name__)

#------------------------------------------------------
# POST: /api/login
#------------------------------------------------------
# @bp_api_login.post('login')
@bp_api_login.post('')
def login():
    login_attempt = services.auth.attempt_login()

    if not login_attempt.successful:
        return (str(login_attempt.error), HTTPStatus.BAD_REQUEST)

    return ('', HTTPStatus.OK)

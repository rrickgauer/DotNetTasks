"""
********************************************************************************************

Url Prefix: /api/user

api endpoints

********************************************************************************************
"""

from __future__ import annotations
import flask
from tasks import services
from http import HTTPStatus

# module blueprint
bp_api_user = flask.Blueprint('api_user', __name__)

#------------------------------------------------------
# POST: /api/user/signup
#------------------------------------------------------
@bp_api_user.post('signup')
def sign_up():
    return 'sign up'

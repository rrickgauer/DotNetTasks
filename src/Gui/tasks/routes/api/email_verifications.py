"""
********************************************************************************************

Url Prefix: /api/email-verifications

********************************************************************************************
"""

from __future__ import annotations
import flask
from tasks.common import security
from tasks import services
from http import HTTPStatus

# module blueprint
bp_api_email_verifications = flask.Blueprint('api_email_verifications', __name__)

#------------------------------------------------------
# POST: /api/email-verifications
#------------------------------------------------------
@bp_api_email_verifications.post('')
@security.login_required
def post():
    response = services.email_verifications.send_email_verification()
    return (response.text, response.status_code)

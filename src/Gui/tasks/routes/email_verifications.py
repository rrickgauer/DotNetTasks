"""
********************************************************************************************

Url Prefix: /email-verifications

********************************************************************************************
"""

from __future__ import annotations
from uuid import UUID
import flask
from tasks import services
from tasks.common import security

# module blueprint
bp_email_verifications = flask.Blueprint('email_verifications', __name__)

#------------------------------------------------------
# tasks.com/email-verifications/:emailVerificationsId
#------------------------------------------------------
@bp_email_verifications.route('<uuid:email_verification_id>')
def confirm_email(email_verification_id: UUID):
    security.clear_session_values()

    response = services.email_verifications.confirm_email_verification(email_verification_id)

    if not response.ok:
        return (response.text, response.status_code)

    return flask.render_template('pages/email-verifications/index.html')
    
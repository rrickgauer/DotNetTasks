"""
********************************************************************************************

Url Prefix: /api/password

api endpoints

********************************************************************************************
"""

from __future__ import annotations
from http import HTTPStatus
import flask
from tasks.common import security
from tasks import services

# module blueprint
bp_api_password = flask.Blueprint('api_password', __name__)

#------------------------------------------------------
# POST: /api/password
#------------------------------------------------------
@bp_api_password.post('')
@security.login_required
def update_password():
    # get the passwords from the request form data
    passwords = services.update_password.get_update_password_args_from_request()

    # validate them
    validation_result = services.update_password.validate_password_update_values(passwords)

    if not validation_result == services.update_password.ValidatePasswordUpdateResult.VALID:
        output = services.update_password.get_validation_error_response_object(validation_result)
        return (flask.jsonify(output), HTTPStatus.BAD_REQUEST)

    # send api request
    api_response = services.update_password.send_request(passwords.new)

    return (api_response.text, api_response.status_code)
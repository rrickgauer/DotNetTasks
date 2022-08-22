from __future__ import annotations
from enum import Enum
import flask
from tasks.domain import models
from tasks.common import security, serializers
import requests

from tasks.common import ApiUrlBuilder

class ValidatePasswordUpdateResult(Enum):
    VALID                      = 1
    NEW_PASSWORDS_NOT_MATCHING = 2
    CURRENT_PASSWORD_INCORRECT = 3
    MISSING_REQUIRED_FORM_FIELD = 4


#------------------------------------------------------
# Validate the new passwords
#------------------------------------------------------
def validate_password_update_values(passwords: models.UpdatePasswordArgs) -> ValidatePasswordUpdateResult:
    # make sure all fields have a value
    if None in passwords.__dict__.values():
        return ValidatePasswordUpdateResult.MISSING_REQUIRED_FORM_FIELD

    # make sure the new passwords match
    if passwords.confirm != passwords.new:
        return ValidatePasswordUpdateResult.NEW_PASSWORDS_NOT_MATCHING
    
    # make sure the current password value matches the current password
    user_session = security.get_user_session()

    if passwords.current != user_session.password:
        return ValidatePasswordUpdateResult.CURRENT_PASSWORD_INCORRECT

    return ValidatePasswordUpdateResult.VALID


#------------------------------------------------------
# Get the password values from the request form data
#------------------------------------------------------
def get_update_password_args_from_request() -> models.UpdatePasswordArgs:
    form_values = flask.request.form.to_dict()

    serializer = serializers.UpdatePasswordArgsSerializer(form_values)

    return serializer.serialize()

#------------------------------------------------------
# Transform the validation result into a dict
#------------------------------------------------------
def get_validation_error_response_object(validation_result: ValidatePasswordUpdateResult) -> dict:
    result = dict(
        code = validation_result.value
    )

    return result

#------------------------------------------------------
# Send the api request to update the password
#------------------------------------------------------
def send_request(new_password) -> requests.Response:

    url_builder = ApiUrlBuilder()
    url = url_builder.password()

    response = requests.post(
        url    = url,
        data   = dict(password=new_password),
        verify = False,
        auth   = security.get_user_session_tuple(),
    )

    return response

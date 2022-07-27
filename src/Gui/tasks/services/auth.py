from __future__ import annotations
from http import HTTPStatus
import flask
import requests
from tasks.common.structs import BaseReturn
from tasks.common import security
from tasks.config import get_config

class AuthErrorMessages:
    MISSING_EMAIL_AND_PASSWORD = 'Email and password form values are required.'
    INVALID_CREDENTIALS        = 'The email and password combination do not match.'

#------------------------------------------------------
# Attempt to log a user in
#------------------------------------------------------
def attempt_login() -> BaseReturn:
    result = BaseReturn(successful=False)

    # clear out any existing session user data
    security.clear_session_values()

    # get the user credentials from the request form
    email = flask.request.form.get('email') or None
    password = flask.request.form.get('password') or None

    # make sure the user provided values for email and password
    if None in [email, password]:
        result.error = AuthErrorMessages.MISSING_EMAIL_AND_PASSWORD
        return result

    # send an api request to ensure the credentials are valid
    if not _are_credentials_valid(email, password):
        result.error = AuthErrorMessages.INVALID_CREDENTIALS
        return result

    # store the user's session data
    security.set_session_values(email, password)

    result.successful = True
    return result

#------------------------------------------------------
# Send the login request to the api
# All it does is use the email/password combo to make a GET: /events to see if the authorization is okay
#
# If the credentials are valid, the API will return a 200 response code.
# Otherwise, it returns a 401.
#
# Returns a bool:
#   true: valid credentials
#   false: invalid credentials
#
# Raises an HTTPError if anything other than a 200/401 status code is returned
#------------------------------------------------------
def _are_credentials_valid(email, password) -> bool:
    config = get_config()

    api_response = requests.get(
        url    = f'{config.URL_API}/events',
        auth   = (email, password),
        verify = False,
    )

    # check if the api responded with either a 200 or a 401 status code
    # if not, something else is wrong besides a bad email/password combo
    if api_response.status_code == HTTPStatus.OK:
        return True
    elif api_response.status_code == HTTPStatus.UNAUTHORIZED:
        return False
    else:
        raise requests.HTTPError(api_response)


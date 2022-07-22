"""
********************************************************************************************

Url Prefix: /api

api endpoints

********************************************************************************************
"""

from __future__ import annotations
import flask
# import requests
import requests
from tasks.config import get_config
from tasks.common import security

# module blueprint
bp_api = flask.Blueprint('api', __name__)

#------------------------------------------------------
# Home page
# tickle.com
#------------------------------------------------------
@bp_api.post('login')
def login():

    print(flask.session.get(security.SessionKeys.EMAIL))
    print(flask.session.get(security.SessionKeys.PASSWORD))

    security.clear_session_values()

    email = flask.request.form.get('email') or None
    password = flask.request.form.get('password') or None

    if None in [email, password]:
        return ('email and password form values are required', 400)

    config = get_config()

    api_response = requests.get(
        url  = f'{config.URL_API}/events',
        auth = (email, password),
        verify=False,
    )

    if not api_response.ok:
        return (api_response.text, api_response.status_code)

    # store the user's session data
    security.set_session_values(email, password)

    return ('', api_response.status_code)

    


    
"""
********************************************************************************************

Url Prefix: /api

api endpoints

********************************************************************************************
"""

from __future__ import annotations
from uuid import UUID
import flask
# import requests
import requests
from tasks.config import get_config
from tasks.common import security
import flaskforward

# module blueprint
bp_api = flask.Blueprint('api', __name__)

#------------------------------------------------------
# Home page
# tickle.com
#------------------------------------------------------
@bp_api.post('login')
def login():
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


@bp_api.put('events/<uuid:event_id>')
@security.login_required
def modify_event(event_id: UUID):
    config = get_config()
    data   = flask.request.form

    response = requests.put(
        url    = f'{config.URL_API}/events/{event_id}',
        auth   = (flask.g.email, flask.g.password),
        data   = data,
        verify = False,
    )

    return (response.text, response.status_code)


    
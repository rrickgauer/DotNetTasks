"""
********************************************************************************************

Url Prefix: /auth

********************************************************************************************
"""

from __future__ import annotations
import flask
from tasks.common import security

# module blueprint
bp_auth = flask.Blueprint('auth', __name__)

#------------------------------------------------------
# tickle.com/auth
# tickle.com/auth/login
#------------------------------------------------------
@bp_auth.route('')
@bp_auth.route('login')
def login():
    # clear out the session values
    security.clear_session_values()

    return flask.render_template('pages/auth/login.html')
    
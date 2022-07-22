"""
********************************************************************************************

Url Prefix: /auth

test routes

********************************************************************************************
"""

from __future__ import annotations
import flask

# module blueprint
bp_auth = flask.Blueprint('auth', __name__)

#------------------------------------------------------
# Home page
# tickle.com
#------------------------------------------------------
@bp_auth.route('')
@bp_auth.route('login')
def login():
    return flask.render_template('pages/auth/login.html')
    
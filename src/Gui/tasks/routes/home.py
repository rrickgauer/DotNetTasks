"""
********************************************************************************************

Url Prefix: /app

********************************************************************************************
"""

from __future__ import annotations
import flask

from tasks.common import security

# module blueprint
bp_home = flask.Blueprint('home', __name__)

#------------------------------------------------------
# tasks.com/app
#------------------------------------------------------
@bp_home.route('')
@security.login_required
def home_page():

    return flask.jsonify(dict(
        email = flask.g.email,
        password = flask.g.password
    ))

    return 'home app page'
    
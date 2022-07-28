"""
********************************************************************************************

Url Prefix: /

********************************************************************************************
"""

from __future__ import annotations
import flask
from tasks.common import security, utilities
from tasks import services
from http import HTTPStatus

# module blueprint
bp_home = flask.Blueprint('home', __name__)

#------------------------------------------------------
# tasks.com
# redirect them to /app page for now...
# this will become the landing page endpoint
#------------------------------------------------------
@bp_home.route('')
# @security.login_required
def landing_page():
    endpoint = flask.url_for('.home_page')
    url = utilities.build_gui_url(endpoint)
    return flask.redirect(url, HTTPStatus.FOUND)


#------------------------------------------------------
# tasks.com/app
#------------------------------------------------------
@bp_home.route('app')
@security.login_required
def home_page():
    data = services.home_page.get_data()
    return flask.render_template('pages/home/index.html', data=data)
    
"""
********************************************************************************************

Url Prefix: /

********************************************************************************************
"""

from __future__ import annotations
import flask
from tasks.common import security, utilities
from tasks import services

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
    # return 'hi'
    endpoint = flask.url_for('.home_page')
    url = utilities.build_gui_url(endpoint)
    return flask.redirect(url, 302)


#------------------------------------------------------
# tasks.com/app
#------------------------------------------------------
@bp_home.route('app')
@security.login_required
def home_page():

    data = services.routines.get_home_page_data()

    return flask.render_template('pages/home/index.html', data=data)
    
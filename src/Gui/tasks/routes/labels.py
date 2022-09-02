"""
********************************************************************************************

Url Prefix: /labels

********************************************************************************************
"""

from __future__ import annotations
import flask
from tasks.common import security, utilities
from tasks import services
from http import HTTPStatus

# module blueprint
bp_labels = flask.Blueprint('labels', __name__)

#------------------------------------------------------
# tasks.com/labels
#------------------------------------------------------
@bp_labels.route('')
@security.login_required
def home_page():
    # data = services.home_page.get_data()
    return flask.render_template('pages/labels/index.html')
    
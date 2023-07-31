"""
********************************************************************************************

Url Prefix: /checklists

********************************************************************************************
"""

from __future__ import annotations
import flask
from tasks.common import security

# module blueprint
bp_checklists = flask.Blueprint('checklists', __name__)

#------------------------------------------------------
# tasks.com/checklists
#------------------------------------------------------
@bp_checklists.route('')
@security.login_required
def checklists_page():
    return flask.render_template('pages/checklists/index.html')
    
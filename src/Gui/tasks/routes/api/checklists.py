"""
********************************************************************************************

Url Prefix: /api/checklists

Endpoints for the api checklists

********************************************************************************************
"""

from __future__ import annotations
import flask
from tasks.common import security
from tasks.services import checklists as checklist_services
from flasklib import responses

# module blueprint
bp_api_checklists = flask.Blueprint('api_checklists', __name__)

#------------------------------------------------------
# GET: /api/checklists
#------------------------------------------------------
@bp_api_checklists.get('')
@security.login_required
def get_checklists():

    checklists = checklist_services.get_checklists()
    html = checklist_services.build_checklists_sidebar_html(checklists)

    return html
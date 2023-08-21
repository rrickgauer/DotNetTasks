"""
********************************************************************************************

Url Prefix: /checklists

********************************************************************************************
"""

from __future__ import annotations
from uuid import UUID
import flask
from tasks.common import security
from tasks.services import checklists as checklist_services

# module blueprint
bp_checklists = flask.Blueprint('checklists', __name__)

#------------------------------------------------------
# tasks.com/checklists
#------------------------------------------------------
@bp_checklists.route('')
@security.login_required
def checklists_page():
    return flask.render_template('pages/checklists/index.html')



#------------------------------------------------------
# tasks.com/checklists/:checklistId
#------------------------------------------------------
@bp_checklists.route('<uuid:checklist_id>')
@security.login_required
def checklist_settings_page(checklist_id: UUID):
    page_view = checklist_services.get_general_checklist_settings_page_view(checklist_id)
    return flask.render_template('pages/checklist-settings/general/index.html', data=page_view)



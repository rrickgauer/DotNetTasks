"""
********************************************************************************************

Url Prefix: /api/checklists/:checklistId/labels

Endpoints for the api checklists

********************************************************************************************
"""

from __future__ import annotations
from uuid import UUID
import flask
from tasks.common import security
from tasks.services.checklist_labels import ChecklistLabelsService

# module blueprint
bp_api_checklist_labels = flask.Blueprint('api_checklist_labels', __name__)


#------------------------------------------------------
# PUT: /api/checklists/:checklistId/labels/:labelId
#------------------------------------------------------
@bp_api_checklist_labels.put('<uuid:label_id>')
@security.login_required
def put_checklist_label(checklist_id: UUID, label_id: UUID):
    
    service = ChecklistLabelsService(checklist_id)
    response = service.assign_label(label_id)

    return (response.content, response.status_code)



#------------------------------------------------------
# DELETE: /api/checklists/:checklistId/labels/:labelId
#------------------------------------------------------
@bp_api_checklist_labels.delete('<uuid:label_id>')
@security.login_required
def delete_checklist_label(checklist_id: UUID, label_id: UUID):
    
    service = ChecklistLabelsService(checklist_id)
    response = service.delete_label(label_id)

    return (response.content, response.status_code)




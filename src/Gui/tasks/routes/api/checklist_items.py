"""
********************************************************************************************

Url Prefix: /api/checklists/:checklistId/items

Endpoints for the api checklist items

********************************************************************************************
"""

from __future__ import annotations
from http import HTTPStatus
from uuid import UUID
import flask
from tasks.common import security
from tasks.services.checklist_items import ChecklistItemService

# module blueprint
bp_api_checklist_items = flask.Blueprint('api_checklist_items', __name__)

#------------------------------------------------------
# GET: /api/checklists/:checklistId/items
#------------------------------------------------------
@bp_api_checklist_items.get('')
@security.login_required
def get_checklist_items(checklist_id: UUID):

    service = ChecklistItemService(checklist_id)
    checklist_items = service.get_checklist_items()

    print(flask.json.dumps(checklist_items, indent=4))


    html = service.get_checklist_items_html(checklist_items)

    return html


    
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
import flasklib

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
    html = service.get_checklist_items_html(checklist_items)

    return html


#------------------------------------------------------
# POST: /api/checklists/:checklistId/items
#------------------------------------------------------
@bp_api_checklist_items.post('')
@security.login_required
def post_checklist_items(checklist_id: UUID):

    service = ChecklistItemService(checklist_id)
    checklist_item = service.create_item(flask.request.form.to_dict())
    html = service.get_checklist_item_html(checklist_item)

    return html


#------------------------------------------------------
# DELETE: /api/checklists/:checklistId/items/:itemId
#------------------------------------------------------
@bp_api_checklist_items.delete('<uuid:checklist_item_id>')
@security.login_required
def delete_item(checklist_id: UUID, checklist_item_id: UUID):

    service = ChecklistItemService(checklist_id)
    response = service.delete_checklist_item(checklist_item_id)
    return (response.text, response.status_code)



#------------------------------------------------------
# PUT: /api/checklists/:checklistId/items/:itemId
#------------------------------------------------------
@bp_api_checklist_items.put('<uuid:checklist_item_id>')
@security.login_required
def put_item(checklist_id: UUID, checklist_item_id: UUID):

    service = ChecklistItemService(checklist_id)
    updated_checklist_item = service.update_checklist_item(checklist_item_id, flask.request.form.to_dict())

    return flasklib.responses.get(updated_checklist_item)
    


    




#------------------------------------------------------
# PUT: /api/checklists/:checklistId/items/:itemId/complete
#------------------------------------------------------
@bp_api_checklist_items.put('<uuid:checklist_item_id>/complete')
@security.login_required
def put_item_complete(checklist_id: UUID, checklist_item_id: UUID):

    service = ChecklistItemService(checklist_id)

    response = service.mark_item_complete(checklist_item_id)

    response_data = response.text

    if response.ok:
        response_data = response.json()
    
    return (response_data, response.status_code)
    

#------------------------------------------------------
# DELETE: /api/checklists/:checklistId/items/:itemId/complete
#------------------------------------------------------
@bp_api_checklist_items.delete('<uuid:checklist_item_id>/complete')
@security.login_required
def delete_item_complete(checklist_id: UUID, checklist_item_id: UUID):
    service = ChecklistItemService(checklist_id)

    response = service.mark_item_incomplete(checklist_item_id)

    return (response.text, response.status_code)


    
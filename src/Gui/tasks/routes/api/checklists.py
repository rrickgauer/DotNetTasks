"""
********************************************************************************************

Url Prefix: /api/checklists

Endpoints for the api checklists

********************************************************************************************
"""

from __future__ import annotations
from http import HTTPStatus
from uuid import UUID
import flask
from tasks.common import security
from tasks.services import checklists as checklist_services
from tasks.apiwrapper import ApiWrapperChecklists

# module blueprint
bp_api_checklists = flask.Blueprint('api_checklists', __name__)

#------------------------------------------------------
# GET: /api/checklists
#------------------------------------------------------
@bp_api_checklists.get('')
@security.login_required
def get_checklists():

    return ('from gui', 400)

    api = ApiWrapperChecklists()
    response = api.get_all()
    
    if not response.ok:
        return (response.json(), response.status_code)


    checklists = checklist_services.get_checklists()
    html = checklist_services.build_checklists_sidebar_html(checklists)

    return html

#------------------------------------------------------
# POST: /api/checklists
#------------------------------------------------------
@bp_api_checklists.post('')
@security.login_required
def post_checklist():
    return checklist_services.create_checklist(flask.request.form.to_dict())


#------------------------------------------------------
# GET: /api/checklists/:checklistId
#------------------------------------------------------
@bp_api_checklists.get('<uuid:checklist_id>')
@security.login_required
def get_checklist(checklist_id: UUID):
    checklist = checklist_services.get_checklist(checklist_id)
    html = checklist_services.get_open_checklist_card_html(checklist)
    return html



#------------------------------------------------------
# DELETE: /api/checklists/:checklistId
#------------------------------------------------------
@bp_api_checklists.delete('<uuid:checklist_id>')
@security.login_required
def delete_checklist(checklist_id: UUID):
    response = checklist_services.delete_checklist(checklist_id)
    return (response.text, response.status_code)


#------------------------------------------------------
# PUT: /api/checklists/:checklistId
#------------------------------------------------------
@bp_api_checklists.put('<uuid:checklist_id>')
@security.login_required
def put_checklist(checklist_id: UUID):
    response = checklist_services.save_checklist(checklist_id, flask.request.form.to_dict())
    return (response.text, response.status_code)


#------------------------------------------------------
# POST: /api/checklists/:checklistId/clones
#------------------------------------------------------
@bp_api_checklists.post('<uuid:checklist_id>/clones')
@security.login_required
def post_checklist_clones(checklist_id: UUID):
    request_data = flask.request.form.to_dict()
    new_checklist = checklist_services.clone_checklist(checklist_id, request_data)

    response = flask.jsonify(new_checklist)
    response.status_code = HTTPStatus.CREATED
    return response
    
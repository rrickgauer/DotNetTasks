from __future__ import annotations
from uuid import UUID
import requests
from tasks.apiwrapper import ApiWrapperChecklists
from tasks.apiwrapper import ApiWrapperChecklistClones
from tasks.domain import models
from tasks.domain.views import GeneralChecklistSettingsPageView
from tasks.domain.views import LabelsChecklistSettingsPageView
from tasks.domain.views import BaseChecklistSettingsPageView
from .checklist_items import ChecklistItemService
from typing import List
from tasks.common.macros import ChecklistsSidebarMacro
from tasks.common.macros import OpenChecklistCardMarco
from markupsafe import Markup
from flasklib.errors import RequestError

def get_checklists() -> List[models.ChecklistResponse]:
    """Get all the user's checklists"""

    api = ApiWrapperChecklists()
    
    response = api.get_all()
    data = response.json()
    
    checklists = models.ChecklistResponse.from_dicts(data)
    
    return checklists


def build_checklists_sidebar_html(checklists: List[models.ChecklistResponse]) -> Markup:
    """Build the sidebar html for the checklists"""

    html = ChecklistsSidebarMacro.render_html(checklists)
    return html



def create_checklist(data: dict) -> dict:
    """Create a new checklist"""

    api = ApiWrapperChecklists()
    response = api.post(data)
    return response.json()



def save_checklist(checklist_id: UUID, data: dict) -> requests.Response:
    """Save the checklist"""
    
    api = ApiWrapperChecklists()
    response = api.put(checklist_id, data)
    return response



def get_general_checklist_settings_page_view(checklist_id: UUID) -> GeneralChecklistSettingsPageView:
    """Get the page view for the general checklist settings page"""
    
    checklist_item_service = ChecklistItemService(checklist_id)

    result = GeneralChecklistSettingsPageView(
        checklist_items = checklist_item_service.get_checklist_items(),
    )

    _set_base_page_view_data(result, checklist_id)

    return result



def get_labels_checklist_settings_page_view(checklist_id: UUID) -> LabelsChecklistSettingsPageView:
    """Get the page view for the labels checklist settings page"""

    result = LabelsChecklistSettingsPageView(
        
    )

    _set_base_page_view_data(result, checklist_id)

    return result


def _set_base_page_view_data(page_view: BaseChecklistSettingsPageView, checklist_id: UUID):
    page_view.checklist = get_checklist(checklist_id)



def get_checklist(checklist_id: UUID):
    """Get the specified checklist"""

    api = ApiWrapperChecklists()
    response_data = api.get(checklist_id).json()
    
    return models.ChecklistResponse.from_dict(response_data)


def get_open_checklist_card_html(checklist) -> Markup:
    """Build the html for the open checklist"""

    return OpenChecklistCardMarco.render_html(checklist)


def delete_checklist(checklist_id: UUID) -> requests.Response:
    """Delete the specified checklist"""

    api = ApiWrapperChecklists()

    try:
        response = api.delete(checklist_id)
    except RequestError as error:
        pass

    return response



def clone_checklist(checklist_id: UUID, data: dict) -> models.ChecklistResponse:
    """Clone the specified checklist"""

    api = ApiWrapperChecklistClones(checklist_id)
    
    response_data = api.post(data).json()
    
    new_checklist = models.ChecklistResponse.from_dict(response_data)

    return new_checklist
    







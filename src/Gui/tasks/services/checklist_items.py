from __future__ import annotations
from uuid import UUID
from markupsafe import Markup
import requests
import typing
from tasks.apiwrapper import ApiWrapperChecklistItems
from tasks.apiwrapper import ApiWrapperChecklistItemComplete
from tasks.domain.models import ChecklistItemResponse
from tasks.common.macros import ChecklistItemsMacro
from tasks.common.macros import ChecklistItemMacro

class ChecklistItemService:

    def __init__(self, checklist_id: UUID):
        """Checklist Item Services"""
        
        self.checklist_id = checklist_id


    def get_checklist_items(self) -> typing.List[ChecklistItemResponse]:
        """Get the checklist items for the current checklist"""

        api = ApiWrapperChecklistItems(self.checklist_id)
        response_data = api.get_all().json()
        return ChecklistItemResponse.from_dicts(response_data)
    

    def get_checklist_items_html(self, checklist_items: typing.List[ChecklistItemResponse]) -> Markup:
        """Get the html for the checklist items"""

        html = ChecklistItemsMacro.render_html(checklist_items)
        return html


    def create_item(self, checklist_item_data: dict) -> ChecklistItemResponse:
        api = ApiWrapperChecklistItems(self.checklist_id)
        response = api.post(checklist_item_data)
        response_data = response.json()
        return ChecklistItemResponse.from_dict(response_data)


    def get_checklist_item_html(self, checklist_item: ChecklistItemResponse) -> Markup:
        return ChecklistItemMacro.render_html(checklist_item)

    def mark_item_complete(self, checklist_item_id: UUID) -> requests.Response:
        """Mark the specified item as complete"""

        return self._update_item_complete(checklist_item_id, True)
    
    def mark_item_incomplete(self, checklist_item_id: UUID) -> requests.Response:
        """Mark the specified item as incomplete"""

        return self._update_item_complete(checklist_item_id, False)

    def _update_item_complete(self, checklist_item_id: UUID, complete: bool) -> requests.Response:
        """Update the specified item's isComplete property value"""

        api = ApiWrapperChecklistItemComplete(self.checklist_id, checklist_item_id)

        if complete:
            return api.put()
        else:
            return api.delete()
        

    def delete_checklist_item(self, checklist_item_id: UUID) -> requests.Response:
        api = ApiWrapperChecklistItems(self.checklist_id)
        response = api.delete(checklist_item_id)
        return response
    

    def update_checklist_item(self, checklist_item_id: UUID, checklist_data: dict) -> ChecklistItemResponse:
        api = ApiWrapperChecklistItems(self.checklist_id)
        
        response = api.put(checklist_item_id, checklist_data)
        response_data = response.json()
        
        updated_item = ChecklistItemResponse.from_dict(response_data)

        return updated_item
        
        

    
    



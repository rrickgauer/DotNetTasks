from __future__ import annotations
from uuid import UUID
from markupsafe import Markup
import requests
import typing
from tasks.apiwrapper import ApiWrapperChecklistItems
from tasks.domain.models import ChecklistItemResponse
from tasks.common.macros import ChecklistItemsMacro



class ChecklistItemService:


    def __init__(self, checklist_id: UUID):
        self.checklist_id = checklist_id


    def get_checklist_items(self) -> typing.List[ChecklistItemResponse]:
        """Get the checklist items for the current checklist"""

        api = ApiWrapperChecklistItems(self.checklist_id)
        response_data = api.get_all().json()
        return ChecklistItemResponse.from_dicts(response_data)
    

    def get_checklist_items_html(self, checklist_items: typing.List[ChecklistItemResponse]) -> Markup:
     html = ChecklistItemsMacro.render_html(checklist_items)
     return html

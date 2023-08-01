from __future__ import annotations
from tasks.apiwrapper import ApiWrapperChecklists
from tasks.domain import models
from typing import List
from tasks.common.macros import ChecklistsSidebarMacro
from markupsafe import Markup

def get_checklists() -> List[models.ChecklistResponse]:
    """Get all the user's checklists"""

    api = ApiWrapperChecklists()
    
    response = api.get_all()
    data = response.json()
    
    checklists = models.ChecklistResponse.from_dicts(data)
    
    return checklists


def build_checklists_sidebar_html(checklists: List[models.ChecklistResponse]) -> Markup:
    html = ChecklistsSidebarMacro.render_html(checklists)
    return html



def create_checklist(data):
    api = ApiWrapperChecklists()
    response = api.post(data)
    return response.json()








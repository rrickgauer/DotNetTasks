

from __future__ import annotations
from flasklib.jinja import JinjaMacro

ChecklistsSidebarMacro = JinjaMacro('checklists-sidebar.html', 'get_checklists_sidebar')
OpenChecklistCardMarco = JinjaMacro('open-checklist-card.html', 'open_checklist_card')
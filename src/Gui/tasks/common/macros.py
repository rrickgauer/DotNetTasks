

from __future__ import annotations
from flasklib.jinja import JinjaMacro

ChecklistsSidebarMacro = JinjaMacro('checklists-sidebar.html', 'get_checklists_sidebar')
OpenChecklistCardMarco = JinjaMacro('open-checklist-card.html', 'open_checklist_card')
ChecklistItemsMacro = JinjaMacro('checklist-items.html', 'get_checklist_items')
ChecklistItemMacro = JinjaMacro('checklist-items.html', 'get_checklist_item')
OpenChecklistLabelsMacro = JinjaMacro('open-checklist-card-labels.html', 'get_open_checklist_labels')
OpenChecklistLabelMacro = JinjaMacro('open-checklist-card-labels.html', 'get_open_checklist_label')
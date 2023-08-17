from __future__ import annotations
from dataclasses import dataclass
from tasks.domain.models import ChecklistResponse
from tasks.domain.models import ChecklistItemResponse
from tasks.domain.models import LabelAssignment
from typing import Optional as Opt
from typing import List
from flasklib.mappers import IMappable


@dataclass
class GeneralChecklistSettingsPageView(IMappable):
    checklist       : Opt[ChecklistResponse]           = None
    checklist_items : Opt[List[ChecklistItemResponse]] = None
    labels          : Opt[LabelAssignment]             = None








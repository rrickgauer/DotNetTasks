

from __future__ import annotations
from dataclasses import dataclass
from .page_view import PageView
from tasks.domain.models import ChecklistResponse
from tasks.domain.models import ChecklistItemResponse
from typing import Optional as Opt
from typing import List



@dataclass
class BaseChecklistSettingsPageView(PageView):
    checklist : Opt[ChecklistResponse] = None


@dataclass
class GeneralChecklistSettingsPageView(BaseChecklistSettingsPageView):
    checklist_items: Opt[List[ChecklistItemResponse]] = None


@dataclass
class LabelsChecklistSettingsPageView(BaseChecklistSettingsPageView):
    checklist      : Opt[ChecklistResponse] = None






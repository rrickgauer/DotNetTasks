

from __future__ import annotations
from dataclasses import dataclass
from .page_view import PageView
from tasks.domain.models import ChecklistResponse
from tasks.domain.models import ChecklistItemResponse
from typing import Optional as Opt
from typing import List


@dataclass
class ChecklistSettingsPageView(PageView):
    checklist      : Opt[ChecklistResponse]           = None
    checklist_items: Opt[List[ChecklistItemResponse]] = None



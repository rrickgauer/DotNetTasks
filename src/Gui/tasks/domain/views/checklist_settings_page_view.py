

from __future__ import annotations
from dataclasses import dataclass
from .page_view import PageView
from tasks.domain.models import ChecklistResponse
from typing import Optional as Opt


@dataclass
class ChecklistSettingsPageView(PageView):
    checklist: Opt[ChecklistResponse] = None


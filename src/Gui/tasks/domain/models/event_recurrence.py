
from __future__ import annotations
from dataclasses import dataclass
from datetime import date, datetime
from tasks.domain.models.api_responses import EventApiResponse
from flasklib.mappers import IMappable
from typing import Optional as Opt

@dataclass
class EventRecurrence(IMappable):
    event     : Opt[EventApiResponse] = None
    labels    : Opt[list]             = None
    occursOn  : Opt[date]         = None
    completed : Opt[bool]             = None
    cancelled : Opt[bool]             = None




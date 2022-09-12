
from __future__ import annotations
from dataclasses import dataclass
from datetime import datetime, time
from uuid import UUID
from tasks.domain.models.api_responses import EventApiResponse

@dataclass
class EventRecurrence:
    event    : EventApiResponse = None
    labels   : list             = None
    occurs_on : datetime        = None
    completed : bool            = None
    cancelled : bool            = None




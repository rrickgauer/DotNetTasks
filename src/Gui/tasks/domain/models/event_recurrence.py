
from __future__ import annotations
from dataclasses import dataclass
from datetime import datetime, time
from uuid import UUID

@dataclass
class EventRecurrence:
    event_id  : UUID     = None
    name      : str      = None
    occurs_on : datetime = None
    starts_at : time     = None
    completed : bool     = None
    cancelled : bool     = None



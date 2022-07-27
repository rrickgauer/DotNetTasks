from __future__ import annotations
from dataclasses import dataclass
from datetime import datetime, time
from uuid import UUID

@dataclass
class Recurrence:
    eventId   : UUID     = None
    name      : str      = None
    occursOn  : datetime = None
    startsAt  : time     = None
    completed : bool     = None

from __future__ import annotations
from dataclasses import dataclass
from datetime import date, datetime
from uuid import UUID

@dataclass
class EventCompletion:
    event_id   : UUID     = None
    on_date    : date     = None
    created_on : datetime = datetime.today()





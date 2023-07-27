
from __future__ import annotations
from dataclasses import dataclass
from datetime import date, datetime
from uuid import UUID
from flasklib.mappers import IMappable
from typing import Optional as Opt

@dataclass
class EventCompletion(IMappable):
    event_id   : Opt[UUID]     = None
    on_date    : Opt[date]     = None
    created_on : Opt[datetime] = datetime.today()





from __future__ import annotations
from dataclasses import dataclass
from datetime import datetime, time
from uuid import UUID


#------------------------------------------------------
# Json object returned from the api for retrieving recurrences
#------------------------------------------------------
@dataclass
class Recurrence:
    eventId   : UUID     = None
    name      : str      = None
    occursOn  : datetime = None
    startsAt  : time     = None
    completed : bool     = None
    cancelled : bool     = None
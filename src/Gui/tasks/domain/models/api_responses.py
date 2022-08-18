from __future__ import annotations
from dataclasses import dataclass
from datetime import datetime, time, date
from uuid import UUID
from tasks.domain.enums import EventFrequency

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


#------------------------------------------------------
# JSON object returned for an event
#------------------------------------------------------
@dataclass
class EventApiResponse:
    id              : UUID           = None
    name            : str            = None
    description     : str            = None
    phoneNumber     : str            = None
    location        : str            = None
    startsOn        : date           = None
    endsOn          : date           = None
    startsAt        : time           = None
    endsAt          : time           = None
    frequency       : EventFrequency = None
    separation      : int            = None
    createdOn       : datetime       = None
    recurrenceDay   : int            = None
    recurrenceWeek  : int            = None
    recurrenceMonth : int            = None



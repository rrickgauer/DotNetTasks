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
    event     : EventApiResponse    = None
    occursOn  : datetime            = None
    completed : bool                = None
    cancelled : bool                = None
    labels    : list[LabelResponse] = None

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



@dataclass
class UserSignUpApiResponseUser:
    id         : UUID     = None
    email      : str      = None
    password   : str      = None
    createdOn : datetime = None

@dataclass
class UserSignUpApiResponse:
    successful: bool = None
    user: UserSignUpApiResponseUser = None
    error: str = None


@dataclass
class LabelResponse:
    id        : UUID     = None
    userId    : UUID     = None
    name      : str      = None
    color     : str      = None
    createdOn : datetime = None

from __future__ import annotations
from dataclasses import dataclass
from datetime import datetime, time, date
from uuid import UUID
from tasks.domain.enums import EventFrequency
from flasklib.mappers import IMappable
from typing import Optional as Opt

#------------------------------------------------------
# Json object returned from the api for retrieving recurrences
#------------------------------------------------------
@dataclass
class RecurrenceResponse(IMappable):
    event     : Opt[EventApiResponse]    = None
    occursOn  : Opt[datetime]            = None
    completed : Opt[bool]                = None
    cancelled : Opt[bool]                = None
    labels    : Opt[list[LabelResponse]] = None

#------------------------------------------------------
# JSON object returned for an event
#------------------------------------------------------
@dataclass
class EventApiResponse(IMappable):
    id              : Opt[UUID]           = None
    name            : Opt[str]            = None
    description     : Opt[str]            = None
    phoneNumber     : Opt[str]            = None
    location        : Opt[str]            = None
    startsOn        : Opt[datetime]       = None
    endsOn          : Opt[datetime]       = None
    startsAt        : Opt[time]           = None
    endsAt          : Opt[time]           = None
    frequency       : Opt[EventFrequency] = None
    separation      : Opt[int]            = None
    createdOn       : Opt[datetime]       = None
    recurrenceDay   : Opt[int]            = None
    recurrenceWeek  : Opt[int]            = None
    recurrenceMonth : Opt[int]            = None


@dataclass
class UserSignUpUserResponse(IMappable):
    id         : Opt[UUID]     = None
    email      : Opt[str]      = None
    password   : Opt[str]      = None
    createdOn  : Opt[datetime] = None

@dataclass
class UserSignUpResponse(IMappable):
    successful: Opt[bool]                      = None
    user      : Opt[UserSignUpUserResponse] = None
    error     : Opt[str]                       = None

@dataclass
class LabelResponse(IMappable):
    id        : Opt[UUID]     = None
    name      : Opt[str]      = None
    color     : Opt[str]      = None
    createdOn : Opt[datetime] = None


@dataclass
class ChecklistResponse(IMappable):
    id         : Opt[UUID]     = None
    title      : Opt[str]      = None
    type       : Opt[str]      = None
    createdOn  : Opt[datetime] = None
    countItems : Opt[int]      = None


@dataclass
class ChecklistItemResponse(IMappable):
    id         : Opt[str]      = None
    content    : Opt[str]      = None
    createdOn  : Opt[datetime] = None
    position   : Opt[int]      = None
    isComplete : Opt[bool]     = None


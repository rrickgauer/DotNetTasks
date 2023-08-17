from __future__ import annotations
from dataclasses import dataclass
from datetime import datetime, time, date
from uuid import UUID
from tasks.domain.enums import EventFrequency
from flasklib.mappers import IMappable
from typing import Optional as Opt



@dataclass
class LabelResponse(IMappable):
    id        : Opt[UUID]     = None
    name      : Opt[str]      = None
    color     : Opt[str]      = None
    createdOn : Opt[datetime] = None


@dataclass
class LabelAssignment(LabelResponse, IMappable):
    isAssigned: Opt[bool] = False


    def from_parent(response: LabelResponse):
        result = LabelAssignment(
            id        = response.id,
            name      = response.name,
            color     = response.color,
            createdOn = response.createdOn
        )

        return result

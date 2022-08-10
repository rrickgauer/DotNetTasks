from __future__ import annotations
import typing
from .models import EventRecurrence


#------------------------------------------------------
# Represents a dictionary made up of:
#   key = date string
#   value = list of event recurrences
#------------------------------------------------------
# event_recurrences_list = typing.List[models.EventRecurrence]
# DailyRecurrenceMapType = typing.Dict[str, event_recurrences_list]

DailyRecurrenceMapType = typing.Dict[str, typing.List[EventRecurrence]]
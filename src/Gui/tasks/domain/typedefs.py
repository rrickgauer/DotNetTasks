from __future__ import annotations
import typing
from .models import EventRecurrence


#------------------------------------------------------
# Represents a dictionary made up of:
#   key = date string
#   value = list of event recurrences
#------------------------------------------------------
DailyRecurrenceMapType = typing.Dict[str, typing.List[EventRecurrence]]
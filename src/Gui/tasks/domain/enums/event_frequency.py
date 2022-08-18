
from __future__ import annotations
from enum import Enum


class EventFrequency(str, Enum):
    ONCE    = "ONCE"
    DAILY   = "DAILY"
    WEEKLY  = "WEEKLY"
    MONTHLY = "MONTHLY"
    YEARLY  = "YEARLY"
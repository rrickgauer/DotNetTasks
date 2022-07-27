from __future__ import annotations
from dataclasses import dataclass
from datetime import date

@dataclass
class WeekRange:
    start: date = None
    end  : date = None
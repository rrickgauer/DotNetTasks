from __future__ import annotations
from dataclasses import dataclass
from datetime import date
from flasklib.mappers import IMappable
from typing import Optional as Opt

@dataclass
class WeekRange(IMappable):
    start: Opt[date] = None
    end  : Opt[date] = None
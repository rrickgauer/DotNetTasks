
from __future__ import annotations
from dataclasses import dataclass
from flasklib.mappers import IMappable
from typing import Optional as Opt

@dataclass
class UserSession(IMappable):
    email   : Opt[str] = None
    password: Opt[str] = None
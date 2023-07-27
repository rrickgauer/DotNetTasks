
from __future__ import annotations
from dataclasses import dataclass
from flasklib.mappers import IMappable
from typing import Optional as Opt

@dataclass
class UpdatePasswordArgs(IMappable):
    current: Opt[str] = None
    new    : Opt[str] = None
    confirm: Opt[str] = None



from __future__ import annotations
from dataclasses import dataclass


@dataclass
class UpdatePasswordArgs:
    current: str = None
    new    : str = None
    confirm: str = None


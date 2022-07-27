
from __future__ import annotations
from dataclasses import dataclass

@dataclass
class UserSession:
    email   : str = None
    password: str = None
from dataclasses import dataclass
from typing import Any

class ISerialize:
    pass

@dataclass
class BaseReturn:
    successful: bool      = True
    data      : Any       = None
    error     : Exception = None
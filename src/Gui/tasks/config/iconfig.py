
"""
**********************************************************************************************

This is the config class used for the application.

**********************************************************************************************
"""

from __future__ import annotations
from dataclasses import dataclass

@dataclass
class IConfig:
    JSON_SORT_KEYS              : bool = None
    JSONIFY_PRETTYPRINT_REGULAR : bool = None
    SEND_FILE_MAX_AGE_DEFAULT   : int  = None
    SECRET_KEY_API              : str  = None
    SECRET_KEY_GUI              : str  = None
    SECURITY_HEADER_KEY         : str  = None
    SECURITY_HEADER_VALUE       : str  = None
    URL_API                     : str  = None
    URL_GUI                     : str  = None
    IS_PRODUCTION               : bool = None
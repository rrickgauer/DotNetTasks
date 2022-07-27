"""

This module contains all the custom Flask Template Filters.

A custom template filter is basically a custom function that a jinja template can utilize.

See: https://flask.palletsprojects.com/en/2.1.x/templating/#registering-filters

"""

from __future__ import annotations
from datetime import datetime, time
from tasks.common import dates

#------------------------------------------------------
# Filter name: format_date
#------------------------------------------------------
def format_date(date_val: str, format_token) -> str:
    d = datetime.fromisoformat(date_val)
    return dates.format_date(d, format_token)

#------------------------------------------------------
# Filter name: format_time_obj
#------------------------------------------------------
def format_time_obj(time_obj: time, format_token) -> str:
    return dates.format_date(time_obj, format_token)
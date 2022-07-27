from __future__ import annotations
from datetime import datetime, time, date
from tasks.common import dates


def format_date(date_val: str, format_token):
    d = datetime.fromisoformat(date_val)
    return dates.format_date(d, format_token)

def format_time_obj(time_obj: time, format_token):
    return dates.format_date(time_obj, format_token)
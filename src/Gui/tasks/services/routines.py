
from __future__ import annotations
from datetime import datetime
import flask

def get_home_page_data() -> dict:
    current_date = _get_current_date()

    result = dict(
        current_date = current_date,
    )
    
    return result


def _get_current_date() -> str:
    try:
        current_date = datetime.fromisoformat(flask.request.args.get('d') or None)
    except ValueError:
        current_date = datetime.today()

    return current_date.date()




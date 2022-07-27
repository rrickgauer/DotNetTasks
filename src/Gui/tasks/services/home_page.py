
from __future__ import annotations
from datetime import date, datetime
import flask

def get_data() -> dict:
    current_date = _get_current_date()

    result = dict(
        current_date = current_date,
    )
    
    return result

# Gets the current d value from the url search parm
def _get_current_date() -> date:
    try:
        current_date = datetime.fromisoformat(flask.request.args.get('d') or None)
    except Exception:
        current_date = datetime.today()

    return current_date.date()





from __future__ import annotations
from datetime import date, datetime
import flask
from tasks.common import dates

def get_data() -> dict:
    current_date = _get_current_date()

    result = dict(
        current_date = current_date,
        next_week_date = dates.get_weeks_interval(current_date, 1),
        previous_week_date = dates.get_weeks_interval(current_date, -1),
    )

    print(flask.json.dumps(result, indent=4))
    
    return result

# Gets the current d value from the url search parm
def _get_current_date() -> date:
    try:
        current_date = datetime.fromisoformat(flask.request.args.get('d') or None)
    except Exception:
        current_date = datetime.today()

    return current_date.date()




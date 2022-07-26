
from __future__ import annotations
from datetime import datetime
import flask

def get_home_page_data() -> dict:

    current_date = flask.request.args.get('d', datetime.today().date())

    result = dict(
        current_date = current_date,
    )
    
    return result





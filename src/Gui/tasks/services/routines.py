
from __future__ import annotations
from datetime import datetime

def get_home_page_data() -> dict:

    current_date = datetime.today().date()

    result = dict(
        current_date = current_date,
    )
    
    return result





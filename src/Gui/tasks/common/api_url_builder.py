

from datetime import date
from tasks.config.routines import get_config


class ApiUrlBuilder:

    def __init__(self):
        self.config = get_config()
        self.api_url = self.config.URL_API
        

    def cancellations(self, event_id, day: date) -> str:
        return f'{self.api_url}/cancellations/{event_id}/{day.isoformat()}'
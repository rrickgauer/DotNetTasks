"""
********************************************************************************************

This class can build all the urls for the api.

********************************************************************************************
"""

from __future__ import annotations
from datetime import date
from tasks.config.routines import get_config

class ApiUrlBuilder:

    def __init__(self):
        self.config = get_config()
        self.api_url = self.config.URL_API

    #------------------------------------------------------
    # Build the cancellations api url
    #------------------------------------------------------
    def cancellations(self, event_id, day: date) -> str:
        return f'{self.api_url}/cancellations/{event_id}/{day.isoformat()}'

    #------------------------------------------------------
    # Build the events api url
    #------------------------------------------------------
    def events(self, event_id) -> str:
        return f'{self.api_url}/events/{event_id}'
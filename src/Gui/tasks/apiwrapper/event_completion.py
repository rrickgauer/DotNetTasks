from __future__ import annotations
from uuid import UUID
from datetime import date
import requests
from .base import ApiWrapperBase
from . import routines as apiroutines

class ApiWrapperEventCompletion(ApiWrapperBase):

    def __init__(self, event_id: UUID, day: date):
        """Wrapper for the external event completion api resources"""

        super().__init__()

        self._event_id = event_id
        self._day = day
        self._url = self._url_builder.completions(self._event_id, self._day)

    
    def put(self) -> requests.Response:
        """Send a PUT request"""
        return apiroutines.request_put(self._url)
    
    def delete(self) -> requests.Response:
        return apiroutines.request_delete(self._url)



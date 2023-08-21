from __future__ import annotations
from uuid import UUID
import requests
from .base import ApiWrapperBase
from . import routines as apiroutines

class ApiWrapperEventLabels(ApiWrapperBase):

    def __init__(self, event_id: UUID):
        super().__init__()
        self._event_id = event_id
        

    @property
    def url(self) -> str:
        """Endpoint url"""
        return self._url_builder.event_labels(self._event_id)
    

    def get(self) -> requests.Response:
        """GET: /events/:eventId/labels"""
        
        return apiroutines.request_get(
            url = self.url,
        )
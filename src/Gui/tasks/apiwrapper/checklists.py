from __future__ import annotations
from uuid import UUID
import requests
from .base import ApiWrapperBase
from . import routines as apiroutines

class ApiWrapperChecklists(ApiWrapperBase):

    def __init__(self):
        """Wrapper for the external checklist api resources"""

        super().__init__()
        self._url = self.url_builder.checklists()


    def get_all(self) -> requests.Response:
        """Send a GET request for all checklists"""
        return apiroutines.request_get(self._url)


    def post(self, data) -> requests.Response:
        """Send a POST request"""
        return apiroutines.request_post(self._url, data=data)


    def get(self, checklist_id: UUID) -> requests.Response:
        """Send a GET request for a single checklist"""
        url = self._build_url(checklist_id)
        return apiroutines.request_get(url)
    

    def delete(self, checklist_id: UUID) -> requests.Response:
        """Send a DELETE request"""
        url = self._build_url(checklist_id)
        return apiroutines.request_delete(url)
    

    def put(self, checklist_id: UUID, data: dict) -> requests.Response:
        """Send a PUT request"""
        
        url = self._build_url(checklist_id)
        return apiroutines.request_put(url=url, data=data)    

    def _build_url(self, checklist_id: UUID) -> str:
        """Build the appropriate uri for a single checklist resource"""
        return f'{self._url}/{checklist_id}'



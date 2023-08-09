from __future__ import annotations
from uuid import UUID
import requests
from .base import ApiWrapperBase
from . import routines as apiroutines


class ApiWrapperChecklistItems(ApiWrapperBase):

    def __init__(self, checklist_id: UUID):
        """Wrapper for Checklist Items api resources."""

        super().__init__()
        
        self.checklist_id = checklist_id
        self.url = self.url_builder.checklist_items(self.checklist_id)
    
    def get_all(self) -> requests.Response:
        """Send a GET request"""

        return apiroutines.request_get(self.url)
    
    def post(self, data: dict) -> requests.Response:
        """Send a POST request"""
        
        return apiroutines.request_post(
            url = self.url,
            data = data,
        )
    
    def delete(self, checklist_item_id: UUID) -> requests.Response:
        """Send DELETE request"""

        url = self._get_url(checklist_item_id)
        return apiroutines.request_delete(url)
    
    def put(self, checklist_item_id: UUID, data: dict) -> requests.Response:
        """Send PUT request to external api"""
        
        url = self._get_url(checklist_item_id)
        
        return apiroutines.request_put(
            url = url,
            data = data
        )
    

    def _get_url(self, checklist_item_id: UUID) -> str:
        """Build the appropriate uri for a single checklist item"""
        
        return f'{self.url}/{checklist_item_id}'
    

    




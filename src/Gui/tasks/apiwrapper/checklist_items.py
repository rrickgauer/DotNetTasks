from __future__ import annotations
from uuid import UUID
import requests
from .base import ApiWrapperBase


class ApiWrapperChecklistItems(ApiWrapperBase):

    def __init__(self, checklist_id: UUID):
        
        super().__init__()
        
        self.checklist_id = checklist_id
        self.url = self.url_builder.checklist_items(self.checklist_id)
    
    def get_all(self) -> requests.Response:
        return self._get_request(self.url)
    
    def post(self, data: dict) -> requests.Response:
        return self._post_request(
            url = self.url,
            data = data,
        )
    
    def delete(self, checklist_item_id: UUID) -> requests.Response:
        url = f'{self.url}/{checklist_item_id}'
        return self._delete_request(url)
    
    def put(self, checklist_item_id: UUID, data: dict) -> requests.Response:
        """Send PUT request to external api"""
        
        url = f'{self.url}/{checklist_item_id}'
        
        return self._put_request(
            url = url,
            data = data
        )
        
    
    

    




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
        
    
    

    




from __future__ import annotations
from uuid import UUID
import requests
from .base import ApiWrapperBase


class ApiWrapperChecklistClones(ApiWrapperBase):

    def __init__(self, checklist_id: UUID):
        
        super().__init__()
        self.checklist_id = checklist_id
        self.url = self.url_builder.checklist_clones(self.checklist_id)
    
    def post(self, data: dict) -> requests.Response:
        return self._post_request(self.url, data=data)
    

    




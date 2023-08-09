from __future__ import annotations
from uuid import UUID
import requests
from .base import ApiWrapperBase
from . import routines as apiroutines


class ApiWrapperChecklistClones(ApiWrapperBase):

    def __init__(self, checklist_id: UUID):
        
        super().__init__()
        self.checklist_id = checklist_id
        self.url = self.url_builder.checklist_clones(self.checklist_id)
    
    def post(self, data: dict) -> requests.Response:
        return apiroutines.request_post(self.url, data=data)
    

    




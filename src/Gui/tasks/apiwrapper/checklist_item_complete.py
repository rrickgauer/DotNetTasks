from __future__ import annotations
from uuid import UUID
import requests
from .base import ApiWrapperBase


class ApiWrapperChecklistItemComplete(ApiWrapperBase):

    def __init__(self, checklist_id: UUID, checklist_item_id: UUID):
        """Constructor"""

        super().__init__()
        
        self.checklist_id = checklist_id
        self.checklist_item_id = checklist_item_id

    @property
    def url(self):
        return self.url_builder.checklist_item_complete(self.checklist_id, self.checklist_item_id)


    def put(self) -> requests.Response:
        return self._put_request(self.url)
    
    def delete(self) -> requests.Response:
        return self._delete_request(self.url)
        
    
    

    




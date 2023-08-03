from __future__ import annotations
from uuid import UUID
import requests
from .base import ApiWrapperBase


class ApiWrapperChecklists(ApiWrapperBase):

    def __init__(self):
        super().__init__()
        self.url = self.url_builder.checklists()

    def get_all(self) -> requests.Response:
        return self._get_request(self.url)
    
    def post(self, data) -> requests.Response:
        return self._post_request(self.url, data=data)
    
    def get(self, checklist_id: UUID) -> requests.Response:
        url = f'{self.url}/{checklist_id}'
        return self._get_request(url)
    
    def delete(self, checklist_id: UUID) -> requests.Response:
        url = f'{self.url}/{checklist_id}'
        return self._delete_request(url)
    

    def put(self, checklist_id: UUID, data: dict) -> requests.Response:
        
        url = f'{self.url}/{checklist_id}'
        
        return self._put_request(
            url = url,
            data = data,
        )


from __future__ import annotations
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
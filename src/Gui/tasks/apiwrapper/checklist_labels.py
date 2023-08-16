from __future__ import annotations
from uuid import UUID
import requests
from .base import ApiWrapperBase
from . import routines as apiroutines



class ApiWrapperChecklistLabels(ApiWrapperBase):

    def __init__(self, checklist_id: UUID):
        
        super().__init__()
        self._checklist_id = checklist_id

    @property
    def url(self) -> str:
        return self._url_builder.checklist_labels(self._checklist_id)
    
    def get_all(self) -> requests.Response:
        return apiroutines.request_get(
            url = self.url,
        )
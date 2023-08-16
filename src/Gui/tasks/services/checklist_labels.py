
from __future__ import annotations
from uuid import UUID
from typing import List
from tasks.domain.models import LabelResponse
from tasks.apiwrapper import ApiWrapperChecklistLabels


Labels = List[LabelResponse]

class ChecklistLabelsService:

    def __init__(self, checklist_id: UUID):
        self._checklist_id = checklist_id
        self._api = ApiWrapperChecklistLabels(self._checklist_id)


    def get_labels(self) -> Labels:
        """Get the assigned labels for the checklist"""

        # request data from api
        response = self._api.get_all()
        
        # serialize the response into domain objects
        data = response.json()
        models = LabelResponse.from_dicts(data)

        return models
    




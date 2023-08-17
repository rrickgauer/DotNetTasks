from __future__ import annotations
from uuid import UUID
from typing import List
import requests
from tasks.domain.models import LabelResponse
from tasks.domain.models import LabelAssignment
from tasks.apiwrapper import ApiWrapperChecklistLabels
from . import labels as labels_service


class ChecklistLabelsService:

    def __init__(self, checklist_id: UUID):
        self._checklist_id = checklist_id
        self._api = ApiWrapperChecklistLabels(self._checklist_id)


    def get_labels(self) -> List[LabelAssignment]:
        """Get the assigned labels for the checklist"""

        labels = self._get_user_label_assignments()
        assigned_label_ids = self._get_checklist_label_ids()

        # if the label's id exists in the assigned label id list, then it has been assigned 
        for label in labels:
            if label.id in assigned_label_ids:
                label.isAssigned = True

        return labels



    def _get_user_label_assignments(self) -> List[LabelAssignment]:
        """Get a list of label assignments, all initially set to false"""

        assignments = []

        for label in labels_service.get_labels().data:
            assignment = LabelAssignment.from_parent(label)
            assignments.append(assignment)

        return assignments


    def _get_checklist_label_ids(self) -> List[UUID]:
        """Get the ids of each label that has been assigned to the current checklist"""

        # request data from api
        response = self._api.get_all()
        
        # serialize the response into domain objects
        data = response.json()
        checklist_labels = LabelResponse.from_dicts(data)

        ids = [l.id for l in checklist_labels]

        return ids
    


    def assign_label(self, label_id: UUID) -> requests.Response:
        return self._api.put(label_id)

    def delete_label(self, label_id: UUID) -> requests.Response:
        return self._api.delete(label_id)


    




"""
********************************************************************************************

This class can build all the urls for the api.

********************************************************************************************
"""

from __future__ import annotations
from datetime import date
from uuid import UUID
from tasks.config.routines import get_config

class ApiUrlBuilder:

    def __init__(self):
        self.config = get_config()
        self.api_url = self.config.URL_API

    #------------------------------------------------------
    # Build the cancellations api url
    #------------------------------------------------------
    def cancellations(self, event_id, day: date) -> str:
        return f'{self.api_url}/cancellations/{event_id}/{day.isoformat()}'

    #------------------------------------------------------
    # Build the events api url
    #------------------------------------------------------
    def events(self, event_id) -> str:
        return f'{self.api_url}/events/{event_id}'

    def password(self) -> str:
        return f'{self.api_url}/password'
        
    def user(self) -> str:
        return f'{self.api_url}/user'

    def user_sign_up(self) -> str:
        return f'{self.user()}/signup'

    def email_verifications(self) -> str:
        return f'{self.api_url}/email-verifications'

    def email_verifications_confirmation(self, email_verification_id) -> str:
        return f'{self.api_url}/email-verifications/{email_verification_id}/confirm'

    def labels(self) -> str:
        return f'{self.api_url}/labels'

    def label(self, label_id) -> str:
        return f'{self.api_url}/labels/{label_id}'

    def event_labels(self, event_id) -> str:
        return f'{self.events(event_id)}/labels'
    
    def checklists(self) -> str:
        return f'{self.api_url}/checklists'
    
    def checklist_clones(self, checklist_id: UUID):
        return f'{self.api_url}/checklists/{checklist_id}/clones'
    
    def checklist_items(self, checklist_id: UUID):
        return f'{self.api_url}/checklists/{checklist_id}/items'
         
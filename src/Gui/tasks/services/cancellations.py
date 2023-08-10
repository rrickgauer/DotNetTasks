"""
********************************************************************************************

Service methods for event cancellations

********************************************************************************************
"""

from __future__ import annotations
from datetime import date
from uuid import UUID
import requests
from tasks.apiwrapper import ApiWrapperEventCancellations


#------------------------------------------------------
# Send a request to the api for a single event cancellation 
#------------------------------------------------------
def cancel_event(event_id: UUID, day: date) -> requests.Response:
    api = ApiWrapperEventCancellations(event_id, day)
    return api.put()




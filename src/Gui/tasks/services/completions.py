from __future__ import annotations
from dataclasses import asdict
import requests
from tasks.domain import models
from tasks.apiwrapper import ApiWrapperEventCompletion

#------------------------------------------------------
# Create an event completion
#------------------------------------------------------
def create_completion(completion: models.EventCompletion) -> requests.Response:
    validate_event_completion(completion)
    
    api = ApiWrapperEventCompletion(completion.event_id, completion.on_date)
    
    return api.put()

#------------------------------------------------------
# Delete an event completion
#------------------------------------------------------
def remove_completion(completion: models.EventCompletion) -> requests.Response:
    validate_event_completion(completion)

    api = ApiWrapperEventCompletion(completion.event_id, completion.on_date)
    
    return api.delete()



def validate_event_completion(completion: models.EventCompletion):
    if None in asdict(completion).values():
        raise ValueError("Missing a required url parms: event_id or on_date")

from __future__ import annotations
from dataclasses import asdict
import flask
import requests
from tasks.config.routines import get_config
from tasks.common.structs import BaseReturn
from tasks.domain import models
from tasks.common import security

#------------------------------------------------------
# Create an event completion
#------------------------------------------------------
def create_event_completion() -> BaseReturn:
    return _modify_completion('put')

#------------------------------------------------------
# Delete an event completion
#------------------------------------------------------
def delete_event_completion() -> BaseReturn:
    return _modify_completion('delete')

#------------------------------------------------------
# Shared steps to take for sending an api request 
#------------------------------------------------------
def _modify_completion(api_request_method: str) -> BaseReturn:
    result = BaseReturn()

    completion_model = _get_completion_model_from_request()
    response = _send_request(completion_model, api_request_method)

    if not response.ok:
        result.successful = False
        result.error = response
    else:
        result.data = response.text

    return result

#------------------------------------------------------
# Using the specified http request method, send an api request with the given completion object
#------------------------------------------------------
def _send_request(completion: models.EventCompletion, method: str):
    response = requests.request(
        method = method,
        verify = False,
        auth   = security.get_user_session_tuple(),
        url    = _build_api_url(completion),
    )

    return response

#------------------------------------------------------
# Get an EventCompletion model from the request's url values
#------------------------------------------------------
def _get_completion_model_from_request() -> models.EventCompletion:
    result = models.EventCompletion(
        event_id = flask.request.view_args.get('event_id'),
        on_date  = flask.request.view_args.get('on_date'),
    )

    if None in asdict(result).values():
        raise ValueError("Missing a required url parms: event_id or on_date")

    return result

    
#------------------------------------------------------
# Build the url for the /completions resource for the api
#------------------------------------------------------
def _build_api_url(completion: models.EventCompletion) -> str:
    config = get_config()
    url = f'{config.URL_API}/completions/{completion.event_id}/{completion.on_date.isoformat()}'

    return url

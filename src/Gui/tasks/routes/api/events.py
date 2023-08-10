"""
********************************************************************************************

Url Prefix: /api/events

api endpoints

********************************************************************************************
"""

from __future__ import annotations
from dataclasses import asdict
from datetime import date
from uuid import UUID
import flask
from tasks.common import security
from tasks import services
from http import HTTPStatus
import flasklib

# module blueprint
bp_api_events = flask.Blueprint('api_events', __name__)

#------------------------------------------------------
# PUT: /api/events/:event_id
#------------------------------------------------------
@bp_api_events.put('<uuid:event_id>')
@security.login_required
def modify_event(event_id: UUID):
    response = services.events.update_event_from_request(event_id)
    return (response.text, response.status_code)

#------------------------------------------------------
# GET: /api/events/:event_id
#------------------------------------------------------
@bp_api_events.get('<uuid:event_id>')
@security.login_required
def get_event(event_id: UUID):
    result = services.events.send_get_request(event_id)

    if not result.successful:
        return (str(result.error), HTTPStatus.BAD_REQUEST)

    return flask.jsonify(result.data)

#------------------------------------------------------
# DELETE: /api/events/:event_id
#------------------------------------------------------
@bp_api_events.delete('<uuid:event_id>')
@security.login_required
def delete_all_events(event_id: UUID):
    result = services.events.delete_event(event_id)

    if not result.successful:
        return (str(result.error), HTTPStatus.BAD_REQUEST)

    return ('', HTTPStatus.NO_CONTENT)

#------------------------------------------------------
# DELETE: /api/events/:event_id/:date
#------------------------------------------------------
@bp_api_events.delete('<uuid:event_id>/<date:date_val>')
@security.login_required
def delete_single_occurence(event_id: UUID, date_val: date=None):
    services.cancellations.cancel_event(event_id, date_val)
    return flasklib.responses.deleted()


#------------------------------------------------------
# DELETE: /api/events/:event_id/:date/remaining
#
# 1. Cancel the recurrence on the given date
# 2. Modify the event so that it ends on the given date
#------------------------------------------------------
@bp_api_events.delete('<uuid:event_id>/<date:date_val>/remaining')
@security.login_required
def delete_occurence_and_following(event_id: UUID, date_val: date):
    # cancel the event
    services.cancellations.cancel_event(event_id, date_val)

    # fetch the event model from the api
    get_event_request_result = services.events.get_event_model(event_id)

    if not get_event_request_result.successful:
        return (str(get_event_request_result.error), HTTPStatus.BAD_REQUEST)

    # set the endsOn value to occurence date
    event_updated = get_event_request_result.data
    event_updated.endsOn = date_val

    # send the updated event to the api to save it to the database
    update_request_response = services.events.send_put_request(event_id, asdict(event_updated))

    if not update_request_response.ok:
        return (update_request_response.text, update_request_response.status_code)

    return ('', HTTPStatus.NO_CONTENT)
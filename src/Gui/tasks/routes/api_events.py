"""
********************************************************************************************

Url Prefix: /api/events

api endpoints

********************************************************************************************
"""

from __future__ import annotations
from datetime import date
from uuid import UUID
import flask
from tasks.common import security
from tasks import services
from http import HTTPStatus


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
    result = services.events.get_event(event_id)

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
    result = services.cancellations.create_cancellation(event_id, date_val)

    if not result.successful:
        return (str(result.error), HTTPStatus.BAD_REQUEST)

    return ('', HTTPStatus.OK)

#------------------------------------------------------
# DELETE: /api/events/:event_id/:date/remaining
#------------------------------------------------------
@bp_api_events.delete('<uuid:event_id>/<date:date_val>/remaining')
@security.login_required
def delete_occurence_and_following(event_id: UUID, date_val: date=None):
    result = services.events.delete_event(event_id)

    if not result.successful:
        return (str(result.error), HTTPStatus.BAD_REQUEST)

    return ('', HTTPStatus.NO_CONTENT)
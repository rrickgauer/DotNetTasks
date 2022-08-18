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
from tasks.common.url_rules import DeleteEventUrlRules

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
# DELETE: /api/events/:event_id/:date
# DELETE: /api/events/:event_id/:date/remaining
#------------------------------------------------------
@bp_api_events.delete(DeleteEventUrlRules.ALL.value)
@bp_api_events.delete(DeleteEventUrlRules.SINGLE.value)
@bp_api_events.delete(DeleteEventUrlRules.FOLLOWING.value)
@security.login_required
def delete_all_events(event_id: UUID, date_val: date=None):
    result = services.events.delete_event(event_id)

    if not result.successful:
        return (str(result.error), HTTPStatus.BAD_REQUEST)

    return ('', HTTPStatus.NO_CONTENT)
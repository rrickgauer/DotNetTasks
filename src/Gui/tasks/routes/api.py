"""
********************************************************************************************

Url Prefix: /api

api endpoints

********************************************************************************************
"""

from __future__ import annotations
from datetime import date
from uuid import UUID
import flask
from tasks.common import security
from tasks import services
from tasks.common.dates import get_week_range
from http import HTTPStatus
from tasks.common.url_rules import DeleteEventUrlRules

# module blueprint
bp_api = flask.Blueprint('api', __name__)

#------------------------------------------------------
# POST: /api/login
#------------------------------------------------------
@bp_api.post('login')
def login():
    login_attempt = services.auth.attempt_login()

    if not login_attempt.successful:
        return (str(login_attempt.error), HTTPStatus.BAD_REQUEST)

    return ('', HTTPStatus.OK)

#------------------------------------------------------
# PUT: /api/events/:event_id
#------------------------------------------------------
@bp_api.put('events/<uuid:event_id>')
@security.login_required
def modify_event(event_id: UUID):
    response = services.events.update_event_from_request(event_id)
    return (response.text, response.status_code)

#------------------------------------------------------
# GET: /api/events/:event_id
#------------------------------------------------------
@bp_api.get('events/<uuid:event_id>')
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
@bp_api.delete(DeleteEventUrlRules.ALL.value)
@bp_api.delete(DeleteEventUrlRules.SINGLE.value)
@bp_api.delete(DeleteEventUrlRules.FOLLOWING.value)
@security.login_required
def delete_all_events(event_id: UUID, date_val: date=None):
    result = services.events.delete_event(event_id)

    if not result.successful:
        return (str(result.error), HTTPStatus.BAD_REQUEST)

    return ('', HTTPStatus.NO_CONTENT)


#------------------------------------------------------
# GET: /api/recurrences/:date
#------------------------------------------------------
@bp_api.get('recurrences/<date:date_val>')
@security.login_required
def get_recurrences_in_week(date_val: date):
    # fetch the recurrences from the api
    week_range = get_week_range(date_val)
    result = services.recurrences.get_recurrences(week_range)

    if not result.successful:
        raise result.error

    # generate the html for the recurrences board
    html = services.recurrences.get_recurrences_board_html(result.data, date_val)

    return html


#------------------------------------------------------
# PUT: /api/completions/:eventId/:onDate
# DELETE: /api/completions/:eventId/:onDate
#------------------------------------------------------
@bp_api.route('completions/<uuid:event_id>/<date:on_date>', methods=['PUT', 'DELETE'])
@security.login_required
def event_completetions(event_id: UUID, on_date: date):
    
    if flask.request.method == 'DELETE':
        result = services.completions.delete_event_completion()
    else:
        result = services.completions.create_event_completion()

    if not result.successful:
        raise result.error
        return (str(result.error), 400)

    return (result.data, 200)
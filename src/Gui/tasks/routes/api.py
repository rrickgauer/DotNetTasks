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
import requests
from tasks.config import get_config
from tasks.common import security
from tasks import services
from tasks.common.dates import get_week_range

# module blueprint
bp_api = flask.Blueprint('api', __name__)

#------------------------------------------------------
# POST: /api/login
#------------------------------------------------------
@bp_api.post('login')
def login():
    login_attempt = services.auth.attempt_login()

    if not login_attempt.successful:
        return (str(login_attempt.error), 400)

    return ('', 200)


#------------------------------------------------------
# PUT: /api/events/:event_id
#------------------------------------------------------
@bp_api.put('events/<uuid:event_id>')
@security.login_required
def modify_event(event_id: UUID):
    response = services.events.update_event_from_request(event_id)
    return (response.text, response.status_code)


#------------------------------------------------------
# GET: /api/recurrences/:date
#------------------------------------------------------
@bp_api.get('recurrences/<date:date_val>')
@security.login_required
def get_recurrences_in_week(date_val: date):
    week_range = get_week_range(date_val)
    result = services.recurrences.get_recurrences(week_range)

    if not result.successful:
        raise result.error

    output = dict(
        recurrences = result.data
    )

    html = flask.render_template('components/recurrences-board/container.html', data=output)

    return html
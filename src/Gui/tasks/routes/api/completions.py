"""
********************************************************************************************

Url Prefix: /api/completions

api endpoints

********************************************************************************************
"""

from __future__ import annotations
from datetime import date
from http import HTTPStatus
from uuid import UUID
import flask
from tasks.common import security
from tasks import services

# module blueprint
bp_api_completions = flask.Blueprint('api_completions', __name__)

#------------------------------------------------------
# PUT: /api/completions/:eventId/:onDate
# DELETE: /api/completions/:eventId/:onDate
#------------------------------------------------------
@bp_api_completions.route('<uuid:event_id>/<date:on_date>', methods=['PUT', 'DELETE'])
@security.login_required
def event_completetions(event_id: UUID, on_date: date):
    
    if flask.request.method == 'DELETE':
        result = services.completions.delete_event_completion()
    else:
        result = services.completions.create_event_completion()

    if not result.successful:
        return (str(result.error), HTTPStatus.BAD_REQUEST)

    return (result.data, HTTPStatus.OK)
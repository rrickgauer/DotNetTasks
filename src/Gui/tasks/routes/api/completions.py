"""
********************************************************************************************

Url Prefix: /api/completions

api endpoints

********************************************************************************************
"""

from __future__ import annotations
from datetime import date
from uuid import UUID
import flask
import flasklib
from tasks.common import security
from tasks import services
from tasks.domain.models import EventCompletion


# module blueprint
bp_api_completions = flask.Blueprint('api_completions', __name__)

#------------------------------------------------------
# PUT: /api/completions/:eventId/:onDate
# DELETE: /api/completions/:eventId/:onDate
#------------------------------------------------------
@bp_api_completions.route('<uuid:event_id>/<date:on_date>', methods=['PUT', 'DELETE'])
@security.login_required
def event_completetions(event_id: UUID, on_date: date):
    
    completion = EventCompletion(
        event_id = event_id,
        on_date  = on_date,
    )

    if flask.request.method == 'DELETE':
        services.completions.remove_completion(completion)
        return flasklib.responses.deleted()
    
    else:
        response = services.completions.create_completion(completion)
        return flasklib.responses.created(response.text)


    

    
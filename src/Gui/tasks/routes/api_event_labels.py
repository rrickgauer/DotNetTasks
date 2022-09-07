"""
********************************************************************************************

Url Prefix: /api/events/:eventId/labels

api endpoints for event labels

********************************************************************************************
"""

from __future__ import annotations
from dataclasses import asdict
from datetime import date
import json
from uuid import UUID
import flask
from tasks.common import security
from tasks import services
from http import HTTPStatus

# module blueprint
bp_api_event_labels = flask.Blueprint('api_event_labels', __name__)

#------------------------------------------------------
# GET: /api/events/:event_id/labels
#------------------------------------------------------
@bp_api_event_labels.get('labels')
@security.login_required
def get_event_labels(event_id: UUID):
    return 'hey'
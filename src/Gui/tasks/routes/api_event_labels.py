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
from textwrap import indent
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
@bp_api_event_labels.get('')
@security.login_required
def get_event_labels(event_id: UUID):
    labels = services.event_labels.get_event_labels(event_id)
    return flask.jsonify(labels.json())

#------------------------------------------------------
# PUT: /api/events/:event_id/labels
#------------------------------------------------------
@bp_api_event_labels.put('')
@security.login_required
def update_event_labels(event_id: UUID):
    labels = flask.request.json
    response = services.event_labels.update_event_labels(event_id, labels)

    return (response.text, response.status_code)
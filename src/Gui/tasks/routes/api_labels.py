"""
********************************************************************************************

Url Prefix: /api/labels

Endpoints for the labels api calls

********************************************************************************************
"""

from __future__ import annotations
from uuid import UUID
import flask
from tasks.common import security
from tasks import services

# module blueprint
bp_api_labels = flask.Blueprint('api_labels', __name__)

#------------------------------------------------------
# GET: /api/labels
#------------------------------------------------------
@bp_api_labels.get('')
@security.login_required
def get_labels_html():
    # get labels data from api
    labels = services.labels.get_labels()

    labels_html = services.labels.get_labels_html(labels.data)

    return labels_html

#------------------------------------------------------
# GET: /api/labels
#------------------------------------------------------
@bp_api_labels.get('<uuid:label_id>')
@security.login_required
def get_label(label_id: UUID):
    response = services.labels.get_label(label_id)
    return (response.text, response.status_code)

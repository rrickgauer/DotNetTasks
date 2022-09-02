"""
********************************************************************************************

Url Prefix: /api/labels

Endpoints for the labels api calls

********************************************************************************************
"""

from __future__ import annotations
from datetime import date
import flask
from tasks.common import security
from tasks import services
from tasks.common.dates import get_week_range

# module blueprint
bp_api_labels = flask.Blueprint('api_labels', __name__)

#------------------------------------------------------
# GET: /api/labels
#------------------------------------------------------
@bp_api_labels.get('')
@security.login_required
def get_labels_html():
    html = ''


    # get labels data from api
    labels = services.labels.get_labels()


    labels_html = services.labels.get_labels_html(labels.data)

    return labels_html



    # render the template using the macro

    return html
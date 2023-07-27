"""
********************************************************************************************

Url Prefix: /api/recurrences

Endpoints for the recurrences api calls

********************************************************************************************
"""

from __future__ import annotations
from datetime import date
import flask
from tasks.common import security
from tasks import services
from tasks.common.dates import get_week_range

# module blueprint
bp_api_recurrences = flask.Blueprint('api_recurrences', __name__)

#------------------------------------------------------
# GET: /api/recurrences/:date
#------------------------------------------------------
@bp_api_recurrences.get('<date:date_val>')
@security.login_required
def get_recurrences_in_week(date_val: date):
    # fetch the recurrences from the api
    week_range = get_week_range(date_val)

    labels = flask.request.args.get('labels', None)
    result = services.recurrences.get_recurrences(week_range, labels)

    if not result.successful:
        raise result.error

    # generate the html for the recurrences board
    html = services.recurrences.get_recurrences_board_html(result.data, date_val)

    return html
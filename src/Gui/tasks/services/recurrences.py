from __future__ import annotations
from datetime import date
import flask
import requests
from flasklib.errors import RequestError
from tasks.common.structs import BaseReturn
from tasks.domain import models
from tasks.config import get_config
from tasks.common import security
from tasks.common import DailyRecurrencesMapper
from tasks.common import DailyRecurrenceMapType


# Override the BaseReturn.data type 
class GetRecurrencesResult(BaseReturn):
    data: DailyRecurrenceMapType = None


#------------------------------------------------------
# Get the recurrences for the specified WeekRange from the api
#------------------------------------------------------
def get_recurrences(week_range: models.WeekRange, labels=None) -> GetRecurrencesResult:
    result = GetRecurrencesResult(successful=True)

    api_response      = _get_recurrences_from_api(week_range, labels)
    data              = api_response.json()
    event_recurrences = models.EventRecurrence.from_dicts(data)
    result.data       = _create_date_range_map(event_recurrences, week_range)

    return result

#------------------------------------------------------
# Send a GET recurrences request to the api
#------------------------------------------------------
def _get_recurrences_from_api(week_range: models.WeekRange, labels=None) -> requests.Response:
    # setup url
    config = get_config()
    api_url   = f'{config.URL_API}/recurrences'
    
    # setup url query parms
    parms = dict(
        startsOn = week_range.start,
        endsOn = week_range.end,
    )

    if labels != None:
        parms['labels'] = labels

    # setup auth
    auth = security.get_user_session_tuple()

    # send request to the api
    api_response = requests.get(
        verify = False,
        auth   = auth,
        url    = api_url,
        params = parms,
    )

    if not api_response.ok:
        raise RequestError(api_response)

    return api_response


#------------------------------------------------------
# Create a DailyRecurrenceMap for the specified event recurrences
#------------------------------------------------------
def _create_date_range_map(recurrences: list[models.EventRecurrence], week_range: models.WeekRange) -> DailyRecurrenceMapType:
    mapper = DailyRecurrencesMapper()

    # add all the recurrence to the mapper
    for recurrence in recurrences:
        mapper.add(recurrence)

    result = mapper.map_to_range(week_range)

    return result

#------------------------------------------------------
# Generate the html for the recurrences board
#------------------------------------------------------
def get_recurrences_board_html(recurrences: DailyRecurrenceMapType, recurrence_date: date) -> str:
    # setup the argument data
    output = dict(
        recurrences = recurrences,
        recurrence_date = recurrence_date.isoformat(),
    )

    # load up the template macro
    recurrences_board_macro = flask.get_template_attribute('macros/recurrences.html', 'recurrences_board')

    # call it
    html = recurrences_board_macro(output)

    return html
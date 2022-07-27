from __future__ import annotations
from datetime import date, timedelta
import requests
from tasks.common.structs import BaseReturn
from tasks.domain import models
from tasks.config import get_config
from tasks.common import security
from tasks.common import serializers

SUNDAY_WEEKDAY_INDEX = 6

#------------------------------------------------------
# Get a week range from the specified date
#------------------------------------------------------
def get_week_range(date_val: date) -> models.WeekRange:
    # calculate monday
    monday_diff = timedelta(days = date_val.weekday())
    monday = date_val - monday_diff

    # calculate sunday
    sunday_diff = timedelta(days = (SUNDAY_WEEKDAY_INDEX - date_val.weekday()))
    sunday = date_val + sunday_diff

    week_range = models.WeekRange(
        start = monday,
        end   = sunday,
    )

    return week_range
    

def get_recurrences(week_range: models.WeekRange) -> BaseReturn:
    result = BaseReturn(successful=True)

    try:
        api_response = _get_recurrences_from_api(week_range)
        event_recurrences =  _serialize_api_response(api_response)

        result.data = event_recurrences
    except Exception as ex:
        result.successful = False
        result.error = ex
    
    return result




#------------------------------------------------------
# Send a GET recurrences request to the api
#------------------------------------------------------
def _get_recurrences_from_api(week_range: models.WeekRange) -> requests.Response:
    # setup url
    config = get_config()
    api_url   = f'{config.URL_API}/recurrences'
    
    # setup url query parms
    parms = dict(
        startsOn = week_range.start,
        endsOn = week_range.end,
    )

    # setup auth
    auth = security.get_user_session_tuple()

    api_response = requests.get(
        verify = False,
        auth   = auth,
        url    = api_url,
        params = parms,
    )

    return api_response

#------------------------------------------------------
# Serialize the give recurrence api response into a list of EventRecurrence objects
#------------------------------------------------------
def _serialize_api_response(response: requests.Response) -> list[models.EventRecurrence]:
    event_recurrences = []

    for d in response.json():
        event_recurrences.append(_to_event_recurrence(d))

    return event_recurrences

#------------------------------------------------------
# Serialize the specified dictionary into an EventRecurrence domain model
#------------------------------------------------------
def _to_event_recurrence(response_dict: dict) -> models.EventRecurrence:
    serializer = serializers.ApiResponseRecurrenceSerializer(response_dict)
    event_recurrence = serializer.to_model()
    
    return event_recurrence






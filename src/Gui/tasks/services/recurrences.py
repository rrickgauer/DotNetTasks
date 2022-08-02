from __future__ import annotations
import requests
from tasks.common.structs import BaseReturn
from tasks.domain import models
from tasks.config import get_config
from tasks.common import security
from tasks.common import serializers
from tasks.common import DailyRecurrencesMapper
from tasks.common import DailyRecurrenceMapType


class GetRecurrencesResult(BaseReturn):
    data: DailyRecurrenceMapType = None


#------------------------------------------------------
# Get the recurrences for the specified WeekRange from the api
#------------------------------------------------------
def get_recurrences(week_range: models.WeekRange) -> GetRecurrencesResult:
    result = GetRecurrencesResult(successful=True)

    try:
        api_response      = _get_recurrences_from_api(week_range)
        event_recurrences = _serialize_api_response(api_response)
        result.data       = _create_date_range_map(event_recurrences, week_range)
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

    print("\n" * 10)
    print(f'{api_url}?startsOn={week_range.start}&endsOn={week_range.end}')
    print("\n" * 10)
    
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


#------------------------------------------------------
# Create a DailyRecurrenceMap for the specified event recurrences
#------------------------------------------------------
def _create_date_range_map(recurrences: list[models.EventRecurrence], week_range: models.WeekRange) -> DailyRecurrenceMapType:
    mapper = DailyRecurrencesMapper()

    for recurrence in recurrences:
        mapper.add(recurrence)

    result = mapper.map_to_range(week_range)

    return result

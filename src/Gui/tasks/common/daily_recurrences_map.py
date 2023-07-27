from __future__ import annotations
from datetime import date
# import typing
from tasks.domain.typedefs import DailyRecurrenceMapType
from tasks.domain import models
from tasks.common.dates import get_dates_in_range

# #------------------------------------------------------
# # Represents a dictionary made up of:
# #   key = date string
# #   value = list of event recurrences
# #------------------------------------------------------
# # event_recurrences_list = typing.List[models.EventRecurrence]
# # DailyRecurrenceMapType = typing.Dict[str, event_recurrences_list]

# # event_recurrences_list = typing.List[models.EventRecurrence]
# DailyRecurrenceMapType = typing.Dict[str, typing.List[models.EventRecurrence]]


#------------------------------------------------------
# This is a custom "dictionary" to handle creating a map of daily recurrences.
# Python requires that dictionary keys can't be objects (like a date object)
#------------------------------------------------------
class DailyRecurrencesMapper:
    
    #------------------------------------------------------
    # Constructor
    #------------------------------------------------------
    def __init__(self) -> None:
        self._result = dict()

    #------------------------------------------------------
    # Add the specified event recurrence to storage
    #------------------------------------------------------
    def add(self, recurrence: models.EventRecurrence):
        # get the iso string value of the date
        occurs_on_val = recurrence.occursOn.isoformat()

        # get the existing list of recurrences that occur on this date, if it exists
        # otherwise create an empty list
        existing_recurrences = self._result.get(occurs_on_val, [])

        # add the new recurrence to the list
        existing_recurrences.append(recurrence)

        # update the map
        self._result[occurs_on_val] = existing_recurrences

    #------------------------------------------------------
    # Override [ ] index
    # get a list of recurrences that occur on the specified day
    #------------------------------------------------------
    def __getitem__(self, occurs_on: date) -> list[models.EventRecurrence]:
        return self.get(occurs_on)

    #------------------------------------------------------
    # get a list of recurrences that occur on the specified day
    #------------------------------------------------------
    def get(self, occurs_on: date) -> list[models.EventRecurrence]:
        occurs_on_val = occurs_on.isoformat()
        recurrences = self._result.get(occurs_on_val, [])
        return recurrences
    
    #------------------------------------------------------
    # get a dictionary of all occurences for the specified WeekRange
    #------------------------------------------------------
    def map_to_range(self, week_range: models.WeekRange) -> DailyRecurrenceMapType:
        range_map = dict()

        for day in get_dates_in_range(week_range):
            # get the recurrences for the day
            recurrences_for_day = self.get(day)

            # setup the key
            key = day.isoformat()

            # store the recurrences value for the day key
            range_map[key] = recurrences_for_day 

        return range_map
    
    
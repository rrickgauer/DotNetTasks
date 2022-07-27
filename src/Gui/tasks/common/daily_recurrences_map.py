from __future__ import annotations
from datetime import date
import typing
from tasks.domain import models
from tasks.common.dates import get_dates_in_range

DailyRecurrenceMap = typing.Dict[str, list[models.EventRecurrence]]


class DailyRecurrencesMapper:
    
    #------------------------------------------------------
    # Constructor
    #------------------------------------------------------
    def __init__(self) -> None:
        self._result = dict()

    #------------------------------------------------------
    # Add the specified recurrence to storage
    #------------------------------------------------------
    def add(self, recurrence: models.EventRecurrence):
        # get the iso string value of the date
        occurs_on_val = recurrence.occurs_on.isoformat()

        # get the existing list of recurrences that occur on this date, if it exists
        existing_values = self._result.get(occurs_on_val, [])

        # add the new recurrence to the list
        existing_values.append(recurrence)

        # update the map
        self._result[occurs_on_val] = existing_values

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
    def map_to_range(self, week_range: models.WeekRange) -> DailyRecurrenceMap:
        range_map = dict()

        for day in get_dates_in_range(week_range):
            range_map[day.isoformat()] = self[day]        
        
        return range_map
    
    
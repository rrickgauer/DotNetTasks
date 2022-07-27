from __future__ import annotations
from tasks.domain import models
from datetime import date, timedelta

SUNDAY_WEEKDAY_INDEX = 6

#------------------------------------------------------
# Get a week range from the specified date
#------------------------------------------------------
def get_week_range(day: date) -> models.WeekRange:
    # calculate monday
    monday_diff = timedelta(days = day.weekday())
    monday = day - monday_diff

    # calculate sunday
    sunday_diff = timedelta(days = (SUNDAY_WEEKDAY_INDEX - day.weekday()))
    sunday = day + sunday_diff

    week_range = models.WeekRange(
        start = monday,
        end   = sunday,
    )

    return week_range

#------------------------------------------------------
# get a list of all dates between the specified WeekRange
#------------------------------------------------------
def get_dates_in_range(week_range: models.WeekRange) -> list[date]:
    delta = week_range.end - week_range.start
    
    days = []
    
    for i in range(delta.days + 1):
        day = week_range.start + timedelta(days=i)
        days.append(day)

    return days
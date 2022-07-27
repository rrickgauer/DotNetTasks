from __future__ import annotations
from datetime import datetime
from tasks.domain import models
from datetime import date, timedelta
from enum import Enum

SUNDAY_WEEKDAY_INDEX = 6

#------------------------------------------------------
# Custom date formatting values
#------------------------------------------------------
class DateFormatTokens(str, Enum):
    DATE_LONG = "%A %x"     # Tuesday 07/26/22
    TIME      = "%I:%M %p"       # 10:13 AM

#------------------------------------------------------
# Format the given datetime object to the specified token
#------------------------------------------------------
def format_date(day: datetime, token: DateFormatTokens) -> str:
    try:
        formatted_date = day.strftime(token.value)
    except ValueError as ex:
        formatted_date = str(ex)
    
    return formatted_date

#------------------------------------------------------
# Get a week range from the specified date
#
# For example, if the date of 7/26/2022 is provided:
#   - start = 7/25/2022
#   - end = 7/31/2022
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
# Get a list of all dates between the specified WeekRange
#
# For example, if the given week_range's values are:
#   start = 7/25
#   end = 7/28
# 
# The result would be: [7/25, 7/26, 7/27, 7/28]
#------------------------------------------------------
def get_dates_in_range(week_range: models.WeekRange) -> list[date]:
    delta = week_range.end - week_range.start
    
    days = []
    
    for i in range(delta.days + 1):
        day = week_range.start + timedelta(days=i)
        days.append(day)

    return days



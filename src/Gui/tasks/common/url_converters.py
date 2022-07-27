"""
**********************************************************************************************

This module contains all the custom flask url type converters.

See: https://werkzeug.palletsprojects.com/en/2.2.x/routing/#custom-converters

**********************************************************************************************
"""

from __future__ import annotations
from werkzeug.routing import BaseConverter, ValidationError
from datetime import date


#------------------------------------------------------
# Converts a url value of a date type into a python date object, and vice versa
#------------------------------------------------------
class UrlConverterDate(BaseConverter):
    """Flask url date converter."""
    
    def to_python(self, value) -> date:
        try:
            return date.fromisoformat(value)
        except ValueError as ex:
            raise ValidationError(f'Invalid date value: {value}')

    def to_url(self, date_obj: date) -> str:
        return date_obj.isoformat()

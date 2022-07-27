"""
**********************************************************************************************

Custom flask url type converters.

**********************************************************************************************
"""

from __future__ import annotations
from werkzeug.routing import BaseConverter, ValidationError
from datetime import date


class UrlConverterDate(BaseConverter):
    """Flask url date converter."""

    def to_python(self, value) -> date:
        try:
            parsed_date = date.fromisoformat(value)
            return parsed_date
        except Exception as ex:
            raise ValidationError(f'Invalid date value: {value}')
        except ValueError as ex:
            raise ValidationError(f'Invalid date value: {value}')

    def to_url(self, date_obj: date) -> str:
        return date_obj.isoformat()

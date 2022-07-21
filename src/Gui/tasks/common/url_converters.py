"""
**********************************************************************************************

Custom flask url type converters.

**********************************************************************************************
"""

# from __future__ import annotations
# from werkzeug.routing import BaseConverter, ValidationError
# from tickle.common.domain.enums.watches import TickerTypes, WatchTypes


# class EnumConverterBase(BaseConverter):
#     TickleEnumClass = None

#     def to_python(self, value):
#         try:
#             val_int = int(value)
#             request_type = self.TickleEnumClass(val_int)
#             return request_type
#         except ValueError as ex:
#             raise ValidationError()

#     def to_url(self, obj):
#         return obj.name


# class WatchTypeConverter(EnumConverterBase):
#     TickleEnumClass = WatchTypes

# class TickerTypeConverter(EnumConverterBase):
#     TickleEnumClass = TickerTypes
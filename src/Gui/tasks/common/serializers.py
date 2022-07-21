"""
**********************************************************************************************

This module contains all the serializers.
A serializer transforms a dictionary into a domain model.

**********************************************************************************************
"""

from __future__ import annotations
from dataclasses import dataclass
import datetime
from decimal import Decimal
from typing import Any

#------------------------------------------------------
# Parse the given datetime string into a python datetime/date object
#
# Args:
#   datetime_module: either the datetime.datetime module or the datetime.date module (both have the fromisoformat function)
#   date_string: the date string to parse
#------------------------------------------------------
def parseIsoDatetime(datetime_module, date_string: str=None) -> datetime.datetime | str | None:    
    try:
        result = datetime_module.fromisoformat(date_string)
    except Exception as e:
        result = date_string
    
    return result


def serializeDecimal(decimal_val) -> Decimal | None | Any:
    try:
        result = Decimal(decimal_val)
    except:
        result = decimal_val
    
    return result

#------------------------------------------------------
# Base serializer class
#------------------------------------------------------
class SerializerBase:
    DomainModel: dataclass = object

    #------------------------------------------------------
    # Constructor
    #
    # Args:
    #   - dictionary: a dict of the data to serialize into the Domain Model
    #   - domain_model: an instance of the class' DomainModel or None
    #------------------------------------------------------
    def __init__(self, dictionary: dict, domain_model = None):
        self.dictionary = dictionary
        
        # if the given domain_model is not null, set the object's domain_model field to it
        # otherwise, call the contructor of the class' DomainModel
        self.domain_model = domain_model or self.DomainModel()

    #------------------------------------------------------
    # Serialize the object's dictionary into the sub-class' domain model
    #------------------------------------------------------
    def serialize(self) -> dataclass:
        model = self.domain_model

        # get a list of all the Model's attributes
        model_keys = list(model.__annotations__.keys())

        # if the dict's key is an attribute in the model, copy over the value
        for key, value in self.dictionary.items():
            if not key in model_keys:
                continue
            elif not value:
                setattr(model, key, None)
            else:
                setattr(model, key, value or None)

        return model



# class StocksLibSearchResponseSerializer(SerializerBase):
#     DomainModel = stockslib.StocksApiSearchResponse

#     def serialize(self) -> stockslib.StocksApiSearchResponse:
#         return super().serialize()

        
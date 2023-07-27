"""
**********************************************************************************************

These are the implementations of the IConfig class.
The production implementation simply copies the ConfigBase implementation.
Then, the development one overrides some of the property values.

**********************************************************************************************
"""

from .base import ConfigBase
import copy

# Production
ConfigProduction = copy.deepcopy(ConfigBase)

# Development
ConfigDev = copy.deepcopy(ConfigProduction)

ConfigDev.URL_API       = 'https://localhost:7259'
ConfigDev.URL_GUI       = 'http://127.0.0.1:5020'
ConfigDev.IS_PRODUCTION = False



"""
**********************************************************************************************

Development and production configuration classes

**********************************************************************************************
"""

from .base import ConfigBase

#------------------------------------------------------
# The production config is the default, 
# It inherits all the values from the base
#------------------------------------------------------
class ConfigProduction(ConfigBase):
    pass

#------------------------------------------------------
# Provide any overrides needed for development
#------------------------------------------------------
class ConfigDev(ConfigBase):
    # pass
    URL_API = 'https://localhost:7259'
    URL_GUI = 'http://127.0.0.1:5020'
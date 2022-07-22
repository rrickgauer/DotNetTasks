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
    pass
    # SECRET_KEY_API = b'2JWFQU+LZXsQIiTef21hFMxQ2gdEE2YQwvkd8FztLwUYOVXzSxwuP+tTXenvhOIZ96iowjwSToqQLEU4rUCUBg=='
    # SECRET_KEY_GUI = b'73fT427SmZ/TQhl7lVRvd2fSMHW+LdNPxfS+2xHtn9wjCzSy7OouBvsgoQHbtlfoTUiJUASknvIDWZ35qqoF5g=='
    
    # URL_API = 'http://127.0.0.1:5010/v1'
 


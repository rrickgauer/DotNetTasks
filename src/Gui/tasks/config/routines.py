


import flask
from .base import ConfigBase
from .configs import ConfigDev, ConfigProduction


def get_config() -> ConfigBase:
    return get_correct_config_class(flask.current_app)

#------------------------------------------------------
# Get the appropriate configuration for the specified flask application
#------------------------------------------------------
def get_correct_config_class(flask_app: flask.Flask) -> ConfigBase:
    if flask_app.env == "production":
        return ConfigProduction()
    else:
        return ConfigDev
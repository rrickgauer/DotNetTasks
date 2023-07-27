import flask
from .configs import ConfigDev, ConfigProduction
from .iconfig import IConfig


def get_config() -> IConfig:
    """Get the current config for the application"""
    return get_correct_config_class(flask.current_app)


def get_correct_config_class(flask_app: flask.Flask) -> IConfig:
    """Get the appropriate configuration for the specified flask application"""

    if flask_app.debug:
        return ConfigDev
    
    return ConfigProduction
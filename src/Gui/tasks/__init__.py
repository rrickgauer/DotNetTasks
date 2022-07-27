"""
********************************************************************************************

This is the entry point for running an application.
    - Create a new flask application object
    - Register all the blue prints

********************************************************************************************
"""

from __future__ import annotations
import flask
from . import routes
from .config import get_correct_config_class
from .common import url_converters as url_converters


#------------------------------------------------------
# Setup the custom url type converters for some enums
#------------------------------------------------------
def _setupCustomConverters(flask_app: flask.Flask):
    flask_app.url_map.converters.update(date=url_converters.UrlConverterDate)

#------------------------------------------------------
# Register the blueprints
#------------------------------------------------------
def _registerBlueprints(flask_app: flask.Flask):
    flask_app.register_blueprint(routes.bp_test, url_prefix='/test')
    flask_app.register_blueprint(routes.bp_api, url_prefix='/api')
    flask_app.register_blueprint(routes.bp_auth, url_prefix='/auth')
    flask_app.register_blueprint(routes.bp_home, url_prefix='/')

# Main logic
app = flask.Flask(__name__)
_setupCustomConverters(app)
_registerBlueprints(app)
app_config = get_correct_config_class(app)
app.config.from_object(app_config)
app.json_encoder = app_config.JSON_ENCODER
app.secret_key = app_config.SECRET_KEY_GUI









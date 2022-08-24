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
from .common import dates
from .common import template_filters

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
    flask_app.register_blueprint(routes.bp_auth, url_prefix='/auth')
    flask_app.register_blueprint(routes.bp_account, url_prefix='/account')

    flask_app.register_blueprint(routes.bp_api_login, url_prefix='/api/login')
    flask_app.register_blueprint(routes.bp_api_events, url_prefix='/api/events')
    flask_app.register_blueprint(routes.bp_api_recurrences, url_prefix='/api/recurrences')
    flask_app.register_blueprint(routes.bp_api_completions, url_prefix='/api/completions')
    flask_app.register_blueprint(routes.bp_api_password, url_prefix='/api/password')
    flask_app.register_blueprint(routes.bp_api_user, url_prefix='/api/user')

    flask_app.register_blueprint(routes.bp_home, url_prefix='/')

#------------------------------------------------------
# Register global template data
#------------------------------------------------------
def _register_template_data(flask_app: flask.Flask):
    flask_app.jinja_env.globals.update(
        date_format_tokens = dates.DateFormatTokens,
    )

#------------------------------------------------------
# Add custom template functions
#------------------------------------------------------
def _add_template_filters(flask_app: flask.Flask):
    flask_app.add_template_filter(template_filters.format_date, 'format_date')
    flask_app.add_template_filter(template_filters.format_time_obj, 'format_time_obj')


# Main logic
app = flask.Flask(__name__)

_setupCustomConverters(app)

_registerBlueprints(app)

app_config = get_correct_config_class(app)

app.config.from_object(app_config)

app.json_encoder = app_config.JSON_ENCODER

app.secret_key = app_config.SECRET_KEY_GUI

_register_template_data(app)

_add_template_filters(app)







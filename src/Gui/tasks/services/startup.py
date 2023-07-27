"""
********************************************************************************************

Flask application startup services

********************************************************************************************
"""

from __future__ import annotations
import flask
from tasks import routes
from tasks.common import url_converters
from tasks.common import template_filters
from tasks.common import dates
from tasks.config import get_correct_config_class


class StartupService:
    """Startup service for the flask application"""

    def __init__(self, flask_app: flask.Flask):
        self.app = flask_app

    
    def setup_app(self):
        """Setup the flask application"""

        self._setup_custom_converters()
        self._register_blueprints()
        self._setup_config()
        self._register_template_data()
        self._add_template_filters()

        
    def _setup_custom_converters(self):
        """Setup the custom url type converters for some enums"""

        self.app.url_map.converters.update(date=url_converters.UrlConverterDate)


    def _register_blueprints(self):
        """Register the blueprints"""

        # gui
        self.app.register_blueprint(routes.gui.bp_test, url_prefix='/test')
        self.app.register_blueprint(routes.gui.bp_auth, url_prefix='/auth')
        self.app.register_blueprint(routes.gui.bp_account, url_prefix='/account')
        self.app.register_blueprint(routes.gui.bp_email_verifications, url_prefix='/email-verifications')
        self.app.register_blueprint(routes.gui.bp_labels, url_prefix='/labels')

        # api
        self.app.register_blueprint(routes.api.bp_api_login, url_prefix='/api/login')
        self.app.register_blueprint(routes.api.bp_api_events, url_prefix='/api/events')
        self.app.register_blueprint(routes.api.bp_api_recurrences, url_prefix='/api/recurrences')
        self.app.register_blueprint(routes.api.bp_api_completions, url_prefix='/api/completions')
        self.app.register_blueprint(routes.api.bp_api_password, url_prefix='/api/password')
        self.app.register_blueprint(routes.api.bp_api_user, url_prefix='/api/user')
        self.app.register_blueprint(routes.api.bp_api_email_verifications, url_prefix='/api/email-verifications')
        self.app.register_blueprint(routes.api.bp_api_labels, url_prefix='/api/labels')
        self.app.register_blueprint(routes.api.bp_api_event_labels, url_prefix='/api/events/<uuid:event_id>/labels')

        # home
        self.app.register_blueprint(routes.gui.bp_home, url_prefix='/')


    def _setup_config(self):
        """Setup the application's configuration"""

        app_config = get_correct_config_class(self.app)

        self.app.config.from_object(app_config)
        self.app.json_encoder = app_config.JSON_ENCODER
        self.app.secret_key = app_config.SECRET_KEY_GUI


    def _register_template_data(self):
        """Register global template data"""
        self.app.jinja_env.globals.update(
            date_format_tokens = dates.DateFormatTokens,
        )


    def _add_template_filters(self):
        """Add custom template functions"""
        self.app.add_template_filter(template_filters.format_date, 'format_date')
        self.app.add_template_filter(template_filters.format_time_obj, 'format_time_obj')



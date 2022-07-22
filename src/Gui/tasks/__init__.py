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


#------------------------------------------------------
# Register the blueprints
#------------------------------------------------------
def _registerBlueprints(flask_app: flask.Flask):
    flask_app.register_blueprint(routes.bp_test, url_prefix='/test')
    flask_app.register_blueprint(routes.bp_api, url_prefix='/api')
    flask_app.register_blueprint(routes.bp_auth, url_prefix='/auth')
    flask_app.register_blueprint(routes.bp_home, url_prefix='/app')

# Main logic
app = flask.Flask(__name__)
_registerBlueprints(app)

app_config = get_correct_config_class(app)
app.config.from_object(app_config)
app.secret_key = app_config.SECRET_KEY_GUI









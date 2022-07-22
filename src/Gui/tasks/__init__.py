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



# Main logic
app = flask.Flask(__name__)
_registerBlueprints(app)

app_config = get_correct_config_class(app)
app.config.from_object(app_config)









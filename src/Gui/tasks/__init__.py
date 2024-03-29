"""
********************************************************************************************

This is the entry point for running an application.
    - Create a new flask application object
    - Register all the blue prints

********************************************************************************************
"""

from __future__ import annotations
import flask
from .services.startup import StartupService

import urllib3
urllib3.disable_warnings()

app = flask.Flask(__name__)

startup = StartupService(app)
startup.setup_app()
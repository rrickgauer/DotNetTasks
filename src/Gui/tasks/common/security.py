"""
**********************************************************************************************
Security module.

Handles logic related to authorization and session values.
**********************************************************************************************
"""

from __future__ import annotations
from functools import wraps
import flask
from . import utilities as util
from tasks.domain import models

#------------------------------------------------------
# Session key names for the session
#------------------------------------------------------
class SessionKeys:
    EMAIL    = 'email'
    PASSWORD = 'password'

#------------------------------------------------------
# Decorator function that verifies that the user's session variables are set.
# If they are, save them to the flask.g object.
# Otherwise, redirect them to the login page.
#------------------------------------------------------
def login_required(f):
    @wraps(f)
    def wrap(*args, **kwargs):
        
        # if user is not logged in, redirect to login page
        if not flask.session:
            # build the url for the login page
            endpoint = flask.url_for('auth.login')
            url = util.build_gui_url(endpoint)

            # redirect them to the login page
            return flask.redirect(url, 302)

        # set the flask g object
        flask.g.email    = flask.session.get(SessionKeys.EMAIL)
        flask.g.password = flask.session.get(SessionKeys.PASSWORD)

        return f(*args, **kwargs)

    return wrap


#------------------------------------------------------
# Clear the session values
#------------------------------------------------------
def clear_session_values():
    flask.session.clear()

#------------------------------------------------------
# Set the session values with the given values.
#------------------------------------------------------
def set_session_values(email, password):
    flask.session.setdefault(SessionKeys.EMAIL, email)
    flask.session.setdefault(SessionKeys.PASSWORD, password)

#------------------------------------------------------
# Get the current user session values as a tuple(email, password)
#------------------------------------------------------
def get_user_session_tuple() -> tuple:
    usersession = get_user_session()
    return (usersession.email, usersession.password)

#------------------------------------------------------
# Get the current user session values
#------------------------------------------------------
def get_user_session() -> models.UserSession:
    usersession = models.UserSession(
        email = flask.g.email,
        password = flask.g.password,
    )

    return usersession
from functools import wraps
import flask
import tasks.config as config

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
            prefix = config.get_config().URL_GUI
            suffix = flask.url_for('auth.login')
            url    = f'{prefix}/{suffix}'

            # return flask.redirect('/login', 302)
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
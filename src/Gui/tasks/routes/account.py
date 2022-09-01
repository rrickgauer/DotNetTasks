"""
********************************************************************************************

Url Prefix: /account

********************************************************************************************
"""

from __future__ import annotations
import flask
from tasks.common import security
from tasks import services

# module blueprint
bp_account = flask.Blueprint('account', __name__)

#------------------------------------------------------
# tasks.com/account
#------------------------------------------------------
@bp_account.route('')
@security.login_required
def account_page():

    response = services.user.request_user_info()

    if not response.ok:
        return (response.text, response.status_code)

    output_data = response.json()

    return flask.render_template('pages/account/index.html', data=output_data)
    
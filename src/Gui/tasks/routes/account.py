"""
********************************************************************************************

Url Prefix: /account

********************************************************************************************
"""

from __future__ import annotations
import flask
from tasks.common import security

# module blueprint
bp_account = flask.Blueprint('account', __name__)

#------------------------------------------------------
# tasks.com/account
#------------------------------------------------------
@bp_account.route('')
@security.login_required
def account_page():
    return flask.render_template('pages/account/index.html')
    
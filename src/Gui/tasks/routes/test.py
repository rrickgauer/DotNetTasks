"""
********************************************************************************************

Url Prefix: /test

test routes

********************************************************************************************
"""

from __future__ import annotations
import flask

# module blueprint
bp_test = flask.Blueprint('test', __name__)

#------------------------------------------------------
# Home page
# tickle.com
#------------------------------------------------------
@bp_test.route('')
def test():
    return flask.render_template('pages/home/index.html')
    return 'test'
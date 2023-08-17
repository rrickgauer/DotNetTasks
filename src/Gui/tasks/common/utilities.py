
from __future__ import annotations
from tasks import config
import flask

#------------------------------------------------------
# Build a complete url for a gui endpoint
# assumes the specified endpoint starts with a leading '/'
#------------------------------------------------------
def build_gui_url(endpoint: str) -> str:
    return endpoint
    prefix = config.get_config().URL_GUI
    url    = f'{prefix}{endpoint}'
    return url


def dump(data):
    print(flask.json.dumps(data, indent=4))
        

    
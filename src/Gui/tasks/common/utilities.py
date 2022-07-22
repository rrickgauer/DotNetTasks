
import flask
from tasks import config

# Build a complete url for a gui endpoint
# assumes the specified endpoint starts with a leading '/'
def build_gui_url(endpoint: str) -> str:
    prefix = config.get_config().URL_GUI
    url    = f'{prefix}{endpoint}'
    return url
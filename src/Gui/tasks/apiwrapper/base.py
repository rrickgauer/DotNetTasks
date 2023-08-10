from __future__ import annotations
from .url_builder import ApiUrlBuilder
from tasks.config import get_config
from tasks.config import IConfig


class ApiWrapperBase:

    def __init__(self):
        self._url_builder = ApiUrlBuilder()
        self._config: IConfig = get_config()
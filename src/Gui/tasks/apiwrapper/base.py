

from __future__ import annotations
from tasks.common import ApiUrlBuilder
from tasks.common import security
from tasks.config import get_config
from tasks.config import IConfig
from flasklib.errors import RequestError
import requests


class ApiWrapperBase:

    def __init__(self):
        self.url_builder = ApiUrlBuilder()
        self.config: IConfig = get_config()

    #------------------------------------------------------
    # Send a GET request
    #------------------------------------------------------
    def _get_request(self, url, parms=None, data=None) -> requests.Response:
        return self.send_request(
            url               = url,
            requests_callback = requests.get,
            parms             = parms,
            data              = data,
        )
    

    #------------------------------------------------------
    # Send a PUT request
    #------------------------------------------------------
    def _put_request(self, url, parms=None, data=None) -> requests.Response:
        return self.send_request(
            url               = url,
            requests_callback = requests.put,
            parms             = parms,
            data              = data,
        )
    

    #------------------------------------------------------
    # Send a POST request
    #------------------------------------------------------
    def _post_request(self, url, parms=None, data=None) -> requests.Response:
        return self.send_request(
            url               = url,
            requests_callback = requests.post,
            parms             = parms,
            data              = data,
        )
    

    #------------------------------------------------------
    # Send a DELETE request
    #------------------------------------------------------
    def _delete_request(self, url, parms=None, data=None) -> requests.Response:
        return self.send_request(
            url               = url,
            requests_callback = requests.delete,
            parms             = parms,
            data              = data,
        )


    #------------------------------------------------------
    # Send a request to the api
    #------------------------------------------------------
    def send_request(self, url, requests_callback, parms=None, data=None, authorize: bool=True, check_response: bool=True) -> requests.Response:
        
        response = requests_callback(
            verify = False,
            auth   = self.get_auth(authorize),
            url    = url,
            params = parms,
            data   = data,
        )

        if check_response:
            self._check_response(response)

        return response
    

    def get_auth(self, authorize: bool) -> tuple | None:
        if authorize:
            return security.get_user_session_tuple()
        
        return None


    def _check_response(self, response: requests.Response):
        if not response.ok:
            raise RequestError(response)

from __future__ import annotations
from tasks.common import security
from flasklib.errors import RequestError
import requests
from typing import Callable


#------------------------------------------------------
# Send a GET request
#------------------------------------------------------
def request_get(url: str, parms: dict=None, data: dict=None) -> requests.Response:
    return send_request(
        url               = url,
        requests_callback = requests.get,
        parms             = parms,
        data              = data,
    )


#------------------------------------------------------
# Send a PUT request
#------------------------------------------------------
def request_put(url: str, parms: dict=None, data: dict=None) -> requests.Response:
    return send_request(
        url               = url,
        requests_callback = requests.put,
        parms             = parms,
        data              = data,
    )


#------------------------------------------------------
# Send a POST request
#------------------------------------------------------
def request_post(url: str, parms: dict=None, data: dict=None) -> requests.Response:
    return send_request(
        url               = url,
        requests_callback = requests.post,
        parms             = parms,
        data              = data,
    )


#------------------------------------------------------
# Send a DELETE request
#------------------------------------------------------
def request_delete(url: str, parms: dict=None, data: dict=None) -> requests.Response:
    return send_request(
        url               = url,
        requests_callback = requests.delete,
        parms             = parms,
        data              = data,
    )


#------------------------------------------------------
# Send a request to the api
#------------------------------------------------------
def send_request(url: str, requests_callback: Callable, parms: dict=None, data: dict=None, authorize: bool=True, validate: bool=True) -> requests.Response:
    """Send an external http request

    Args:
        url (str): the url to use
        requests_callback (Callable): a specific requests callbacl (get, put, post, delete, patch)
        parms (dict, optional): URL parms to include in the request. Defaults to None.
        data (dict, optional): data to include in the request body. Defaults to None.
        authorize (bool, optional): should the request include the authorization field?. Defaults to True.
        validate (bool, optional): should the request be checked if it was successful. Defaults to True.

    Returns:
        requests.Response: the response
    """
    
    response = requests_callback(
        verify = False,
        auth   = get_auth(authorize),
        url    = url,
        params = parms,
        data   = data,
    )

    if validate:
        _check_response(response)

    return response


#------------------------------------------------------
# Get the auth tuple if specified
#------------------------------------------------------
def get_auth(authorize: bool) -> tuple | None:
    if authorize:
        return security.get_user_session_tuple()
    
    return None


#------------------------------------------------------
# Check the specified response if it was successful.
# If not, raise a RequestError exception
#------------------------------------------------------
def _check_response(response: requests.Response):
    if not response.ok:
        raise RequestError(response)
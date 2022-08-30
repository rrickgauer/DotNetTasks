"""
********************************************************************************************

Services for email verifications

********************************************************************************************
"""

from __future__ import annotations
from uuid import UUID
from tasks.common.api_url_builder import ApiUrlBuilder
from tasks.common import security
import requests

def confirm_email_verification(email_verification_id: UUID) -> requests.Response:
    
    url_builder = ApiUrlBuilder()
    url = url_builder.email_verifications_confirmation(email_verification_id)
    
    custom_header = security.get_custom_request_header()

    respone = requests.put(
        url    = url,
        verify = False,
        headers = custom_header,
    )

    return respone



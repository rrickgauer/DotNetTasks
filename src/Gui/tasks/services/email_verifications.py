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
    
    url = ApiUrlBuilder().email_verifications_confirmation(email_verification_id)

    respone = requests.put(
        url    = url,
        verify = False
    )

    return respone



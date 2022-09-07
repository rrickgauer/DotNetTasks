# from .api import bp_api as bp_api
from .test import bp_test as bp_test

# pages
from .auth import bp_auth as bp_auth
from .home import bp_home as bp_home
from .account import bp_account as bp_account
from .email_verifications import bp_email_verifications as bp_email_verifications
from .labels import bp_labels as bp_labels

# api
from .api_login import bp_api_login as bp_api_login
from .api_events import bp_api_events as bp_api_events
from .api_recurrences import bp_api_recurrences as bp_api_recurrences
from .api_completions import bp_api_completions as bp_api_completions
from .api_password import bp_api_password as bp_api_password
from .api_user import bp_api_user as bp_api_user
from .api_email_verifications import bp_api_email_verifications as bp_api_email_verifications
from .api_labels import bp_api_labels as bp_api_labels
from .api_event_labels import bp_api_event_labels as bp_api_event_labels
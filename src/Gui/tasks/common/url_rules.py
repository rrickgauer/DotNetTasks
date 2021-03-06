

from enum import Enum



class DeleteEventUrlRules(str, Enum):
    ALL       = 'events/<uuid:event_id>'
    SINGLE    = 'events/<uuid:event_id>/<date:date_val>'
    FOLLOWING = 'events/<uuid:event_id>/<date:date_val>/remaining'




import { EventModalSelectors } from "./event-modal-selectors";


const m_modal = document.getElementById(EventModalSelectors.CONTAINER);

export class EventModalActions
{

    static getModalEventIdAttribute = () => m_modal.getAttribute(EventModalSelectors.Attributes.EVENT_ID);
    static setModalEventIdAttribute = (newEventId) => m_modal.setAttribute(EventModalSelectors.Attributes.EVENT_ID, newEventId);
    static showModal                = () => $(m_modal).modal('show');
    static hideModal                = () => $(m_modal).modal('hide');
    static resetForm                = () => document.getElementById(EventModalSelectors.Form.FORM).reset();
}

import { DateTime } from "../../../lib/luxon";
import { DateTimeUtil } from "../../helpers/datetime";
import { EventModalSelectors } from "./event-modal-selectors";


const m_modal = document.getElementById(EventModalSelectors.CONTAINER);

export class EventModalActions
{
    static getEventIdAttr = () => m_modal.getAttribute(EventModalSelectors.Attributes.EVENT_ID);
    static setEventIdAttr = (newEventId) => m_modal.setAttribute(EventModalSelectors.Attributes.EVENT_ID, newEventId);
    static showModal      = () => $(m_modal).modal('show');
    static hideModal      = () => $(m_modal).modal('hide');
    static resetForm      = () => document.getElementById(EventModalSelectors.Form.FORM).reset();
    static showSpinner    = () => document.getElementById(EventModalSelectors.SPINNER).classList.remove('d-none');
    static hideSpinner    = () => document.getElementById(EventModalSelectors.SPINNER).classList.add('d-none');
    static hideForm       = () => document.getElementById(EventModalSelectors.Form.FORM).classList.add('d-none');
    static showForm       = () => document.getElementById(EventModalSelectors.Form.FORM).classList.remove('d-none');
    static showDeleteForm = () => $(`#${EventModalSelectors.DeleteForm.DROPDOWN}`).collapse('show');
    static hideDeleteForm = () => $(`#${EventModalSelectors.DeleteForm.DROPDOWN}`).collapse('hide');

    /**
     * Get the current datetime
     * @returns {DateTime}
     */
    static getOccurenceDateAttr = () => DateTimeUtil.toDateTime(m_modal.getAttribute(EventModalSelectors.Attributes.OCCURENCE_DATE));

    /**
     * @param {DateTime} newOccurenceDate 
     */
    static setOccurenceDateAttr = (newOccurenceDate) => m_modal.setAttribute(EventModalSelectors.Attributes.OCCURENCE_DATE, newOccurenceDate.toISODate());
}
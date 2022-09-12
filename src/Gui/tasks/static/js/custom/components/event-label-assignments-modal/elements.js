


export class EventLabelAssignmentsElements
{

    constructor()
    {
        /** @type {HTMLDivElement} */
        this.eModal = document.getElementById(EventLabelAssignmentsElements.MODAL);

        /** @type {HTMLDivElement} */
        this.eSectionLoading = this.eModal.querySelector(`.${EventLabelAssignmentsElements.Sections.SPINNER}`);

        /** @type {HTMLFormElement} */
        this.eForm = document.getElementById(EventLabelAssignmentsElements.FORM);

        /** @type {NodeList<HTMLInputElement>} */
        this.eCheckboxes = this.eForm.querySelectorAll(`input[type="checkbox"]`);

        
        /** @type {HTMLButtonElement} */
        this.eSubmitBtn = this.eForm.querySelector(`button[type="submit"]`);
    }


    setEventIdAttr = (eventId) => this.eModal.setAttribute(EventLabelAssignmentsElements.EVENT_ID_ATTR, eventId);
    getEventIdAttr = () => this.eModal.getAttribute(EventLabelAssignmentsElements.EVENT_ID_ATTR);

}

EventLabelAssignmentsElements.MODAL = 'event-label-assignments-modal';

EventLabelAssignmentsElements.Sections = {
    LABELS_FORM: 'section-labels-form',
    SPINNER: 'section-spinner',
}


EventLabelAssignmentsElements.SELECTOR_CHECKED_CHECKBOXES = 'input[name="labels"]:checked';


EventLabelAssignmentsElements.EVENT_ID_ATTR = 'data-js-event-id';

EventLabelAssignmentsElements.CHECKBOX_NAME = 'labels';


EventLabelAssignmentsElements.FORM = 'event-label-assignment-form';
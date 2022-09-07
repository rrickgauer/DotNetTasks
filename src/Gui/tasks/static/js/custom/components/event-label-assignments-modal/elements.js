


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

        /** @type {NodeListOf<HTMLInputElement>} */
        this.eCheckboxes = this.eForm.querySelectorAll(`input[type="checkbox"]`);

        /** @type {HTMLButtonElement} */
        this.eSubmitBtn = this.eForm.querySelector(`button[type="submit"]`);
    }

}

EventLabelAssignmentsElements.MODAL = 'event-label-assignments-modal';

EventLabelAssignmentsElements.Sections = {
    LABELS_FORM: 'section-labels-form',
    SPINNER: 'section-spinner',
}


EventLabelAssignmentsElements.CHECKBOX_NAME = 'labels';


EventLabelAssignmentsElements.FORM = 'event-label-assignment-form';
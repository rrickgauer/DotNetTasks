import { EventModalSelectors } from "./event-modal-selectors";


export class EventModalDeleteForm
{
    constructor()
    {
        /** @type {HTMLFormElement} */
        this.eForm = document.getElementById(EventModalSelectors.DeleteForm.FORM);

        /** @type {HTMLButtonElement} */
        this.eSubmitButton = document.getElementById(EventModalSelectors.DeleteForm.SUBMIT_BTN);

        /** @type {HTMLInputElement} */
        this.eRadioInputAllEvents = document.getElementById(EventModalSelectors.DeleteForm.Radios.ALL_EVENTS);
    }

    /**
     * Get the value of the currently selected radio input
     * @returns {Number}
     */
    getRadioValue = () =>
    {
        const checkedRadio = document.querySelector(`input[name='${EventModalSelectors.DeleteForm.INPUT}']:checked`);
        return parseInt(checkedRadio.value);
    }


}
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
        this.eRadioInputThisEvent = document.getElementById(EventModalSelectors.DeleteForm.Radios.THIS_EVENT);
        
        /** @type {HTMLInputElement} */
        this.eRadioInputThisEventAndFollowing = document.getElementById(EventModalSelectors.DeleteForm.Radios.THIS_EVENT_AND_FOLLOWING);

        /** @type {HTMLInputElement} */
        this.eRadioInputAllEvents = document.getElementById(EventModalSelectors.DeleteForm.Radios.ALL_EVENTS);
    }

    
    getRadioValue = () =>
    {
        const checkedRadio = document.querySelector(`input[name='${EventModalSelectors.DeleteForm.INPUT}']:checked`);
        return checkedRadio.value;
    }


}
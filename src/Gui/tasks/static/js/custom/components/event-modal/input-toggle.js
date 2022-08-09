import { EventFrequencies } from "../../domain/constants/event-frequencies";
import { EventModalForm } from "./event-modal-form";

/**
 * This class is responsible for showing/hiding recurrenc inputs based on the current frequency input value.
 */
export class EventModalInputToggle
{
    constructor()
    {
        this.eForm = new EventModalForm();
    }


    /**
     * Toggle the recurrence inputs.
     */
    toggleInputs = () =>
    {
        const currentValue = this._getCurrentValue();

        switch(currentValue)
        {
            case EventFrequencies.ONCE:
                this._toggleOnce();
                break;
            case EventFrequencies.DAILY:
                this._toggleDaily();
                break;
            case EventFrequencies.WEEKLY:
                this._toggleWeekly();
                break;
            case EventFrequencies.MONTHLY:
                this._toggleMonthly();
                break;
            default:
                this._showAll();
                break;
        }
    }
    
    /**
     * Toggle the inputs for a ONCE frequency
     */
    _toggleOnce = () =>
    {
        this._showAll();

        this._hideElement(this.eForm.inputSeparation);
        this._hideElement(this.eForm.inputRecurrenceDay);
        this._hideElement(this.eForm.inputRecurrenceWeek);
        this._hideElement(this.eForm.inputRecurrenceMonth);

        this.eForm.inputSeparation.value = '1';
    }

    /**
     * Toggle the inputs for a DAILY frequency
     */
    _toggleDaily = () =>
    {
        this._showAll();

        this._hideElement(this.eForm.inputRecurrenceDay);
        this._hideElement(this.eForm.inputRecurrenceWeek);
        this._hideElement(this.eForm.inputRecurrenceMonth);
    }

    /**
     * Toggle the inputs for a WEEKLY frequency
     */
    _toggleWeekly = () =>
    {
        this._showAll();

        this._hideElement(this.eForm.inputRecurrenceWeek);
        this._hideElement(this.eForm.inputRecurrenceMonth);
    }

    /**
     * Toggle the inputs for a MONTHLY frequency
     */
    _toggleMonthly = () =>
    {
        this._showAll();

        this._hideElement(this.eForm.inputRecurrenceMonth);
    }

    /**
     * Show all the inputs
     */
    _showAll = () =>
    {
        this._showElement(this.eForm.inputSeparation);
        this._showElement(this.eForm.inputRecurrenceDay);
        this._showElement(this.eForm.inputRecurrenceWeek);
        this._showElement(this.eForm.inputRecurrenceMonth);
    }

    /**
     * Hide the specified element's parent form group
     * @param {HTMLElement} eInput 
     */
    _hideElement = (eInput) =>
    {
        eInput.closest(EventModalInputToggle.FORM_GROUP).classList.add(EventModalInputToggle.D_NONE);
    }

    /**
     * Show the specified element's parent form group
     * @param {HTMLElement} eInput 
     */
    _showElement = (eInput) =>
    {
        eInput.closest(EventModalInputToggle.FORM_GROUP).classList.remove(EventModalInputToggle.D_NONE);
    }

    /**
     * Get the current Frequency input value
     * @returns {string}
     */
    _getCurrentValue = () => this.eForm.inputFrequency.value;

}


EventModalInputToggle.D_NONE = 'd-none';
EventModalInputToggle.FORM_GROUP = '.form-group';
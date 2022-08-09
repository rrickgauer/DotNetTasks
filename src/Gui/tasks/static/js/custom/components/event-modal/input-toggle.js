import { EventFrequencies } from "../../domain/constants/event-frequencies";
import { EventModalForm } from "./event-modal-form";


export class EventModalInputToggle
{
    constructor()
    {
        this.eForm = new EventModalForm();
    }


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
    

    _toggleOnce = () =>
    {
        this._showAll();

        this._hideElement(this.eForm.inputSeparation);
        this._hideElement(this.eForm.inputRecurrenceDay);
        this._hideElement(this.eForm.inputRecurrenceWeek);
        this._hideElement(this.eForm.inputRecurrenceMonth);

        this.eForm.inputSeparation.value = '1';
    }

    _toggleDaily = () =>
    {
        this._showAll();

        // this._hideElement(this.eForm.inputSeparation);
        this._hideElement(this.eForm.inputRecurrenceDay);
        this._hideElement(this.eForm.inputRecurrenceWeek);
        this._hideElement(this.eForm.inputRecurrenceMonth);
    }

    _toggleWeekly = () =>
    {
        this._showAll();

        // this._hideElement(this.eForm.inputSeparation);
        // this._hideElement(this.eForm.inputRecurrenceDay);
        this._hideElement(this.eForm.inputRecurrenceWeek);
        this._hideElement(this.eForm.inputRecurrenceMonth);
    }

    _toggleMonthly = () =>
    {
        this._showAll();

        // this._hideElement(this.eForm.inputSeparation);
        // this._hideElement(this.eForm.inputRecurrenceDay);
        // this._hideElement(this.eForm.inputRecurrenceWeek);
        this._hideElement(this.eForm.inputRecurrenceMonth);
    }


    _showAll = () =>
    {
        this._showElement(this.eForm.inputSeparation);
        this._showElement(this.eForm.inputRecurrenceDay);
        this._showElement(this.eForm.inputRecurrenceWeek);
        this._showElement(this.eForm.inputRecurrenceMonth);
    }

    /**
     * 
     * @param {HTMLElement} eInput 
     */
    _hideElement = (eInput) =>
    {
        eInput.closest(EventModalInputToggle.FORM_GROUP).classList.add(EventModalInputToggle.D_NONE);
    }

    /**
     * 
     * @param {HTMLElement} eInput 
     */
    _showElement = (eInput) =>
    {
        eInput.closest(EventModalInputToggle.FORM_GROUP).classList.remove(EventModalInputToggle.D_NONE);
    }

    _getCurrentValue = () => this.eForm.inputFrequency.value;

}


EventModalInputToggle.D_NONE = 'd-none';
EventModalInputToggle.FORM_GROUP = '.form-group';
import { DateTime } from "../../lib/luxon";


/**
 * Setup all the date pickers
 */
export function initCustomDatePickers()
{
    flatpickr(`.${DatePicker.CSS_CLASS}`, DatePicker.STANDARD_CONFIG);
}

export class DatePicker
{
    constructor(eInput)
    {
        /** @type {HTMLInputElement} */
        this.eInput = eInput;

        this._flatpickr = this.eInput._flatpickr;
    }


    /**
     * Set the value    
     * @param {DateTime} newValue new value
     */
    setValueFromDateTime = (newValue) =>
    {
        this._flatpickr.setDate(newValue.toISODate(), true);
    }

    /**
     * Set the minimum date value of the element
     * @param {DateTime} minDate new minimum date
     */
    setMinimumDate = (minDate) =>
    {
        this._flatpickr.set('minDate', minDate.toISODate(), true);
    }
}


DatePicker.CSS_CLASS = 'custom-datepicker';

DatePicker.STANDARD_CONFIG = {
    altInput: true,
    altFormat: "F j, Y",
    dateFormat: "Y-m-d",
};






// import { Datepicker } from "vanillajs-datepicker";
import { DateTime } from "../../lib/luxon";


/**
 * Setup all the date pickers
 */
export function initCustomDatePickers()
{
    // flatpickr(`.${DatePicker.CSS_CLASS}`, DatePicker.STANDARD_CONFIG);

    const elements = document.getElementsByClassName(CustomDatepicker.CSS_CLASS);

    for (const e of elements)
    {
        // const sdasda = new Datepicker(e);
    }
}

export class CustomDatepicker
{
    constructor(eInput)
    {
        /** @type {HTMLInputElement} */
        this.eInput = eInput;

        // this._flatpickr = this.eInput._flatpickr;
    }


    /**
     * Set the value    
     * @param {DateTime} newValue new value
     */
    setValueFromDateTime = (newValue) =>
    {
        this.eInput.value = newValue.toISODate();
    }

    /**
     * Set the minimum date value of the element
     * @param {DateTime} minDate new minimum date
     */
    setMinimumDate = (minDate) =>
    {
        // this._flatpickr.set('minDate', minDate.toISODate(), true);
        this.eInput.min = minDate.toISODate();
    }
}


CustomDatepicker.CSS_CLASS = 'custom-datepicker';

CustomDatepicker.STANDARD_CONFIG = {
    altInput: true,
    altFormat: "F j, Y",
    dateFormat: "Y-m-d",
    static: true,
};






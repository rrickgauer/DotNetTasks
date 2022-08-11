

/**
 * Setup all the date pickers
 */
export function initCustomDatePickers()
{
    flatpickr(`.${DatePicker.CSS_CLASS}`, DatePicker.STANDARD_CONFIG);
}


export class DatePicker
{

}


DatePicker.CSS_CLASS = 'custom-datepicker';

DatePicker.STANDARD_CONFIG = {
    altInput: true,
    altFormat: "F j, Y",
    dateFormat: "Y-m-d",
};






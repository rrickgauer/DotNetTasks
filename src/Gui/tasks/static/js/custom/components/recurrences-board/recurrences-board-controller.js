import { RecurrencesBoardActionButtons } from "./recurrences-board-action-buttons";
import { DateTimeUtil } from "../../helpers/datetime";
import { UrlMethods } from "../../helpers/url-methods";
import { DateTime } from "../../../lib/luxon";

export class RecurrencesBoardController
{
    constructor() {
       this.actionButtons = new RecurrencesBoardActionButtons(); 
    }

    /**
     * Set the current value of the date input
     * @param {string} newDateValue - the new date value
     */
    setDateValue = (newDateValue) => {
        this.actionButtons.dateInput.value = newDateValue;
    }

    /**
     * Get the current date input value as a Luxon DateTime
     * @returns {DateTime}
     */
    getDateValue = () => {
        const currentValue = this.actionButtons.dateInput.value;
        return DateTimeUtil.toDateTime(currentValue);
    }


    /**
     * Add the event listeners to the page
     */
    addListeners = () => {
        // set the current recurrences date to today's value
        this.actionButtons.todayButton.addEventListener('click', (e) => {
            this._setDateValueToday();
            this._setUrlDateValueToValue();
        });

        // listen for recurrences date input value changes
        this.actionButtons.dateInput.addEventListener('change', (e) => {
            this._setUrlDateValueToValue();
        });
    }


    /**
     * Set the curernt value of the date input to today's date
     */
    _setDateValueToday = () => {
        const currentDate = DateTimeUtil.getCurrentDateIso();
        this.setDateValue(currentDate);
    }

    /**
     * Set the URL's query parm to the value of the date input
     */
    _setUrlDateValueToValue = () => {
        const newDate = this.actionButtons.dateInput.value;
        const newUrl = UrlMethods.setQueryParmAndRefresh('d', newDate);
        window.location.href = newUrl;
    }
}
import { RecurrencesBoardActionButtons } from "./recurrences-board-action-buttons";
import { DateTimeUtil } from "../../helpers/datetime";

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
     * Set the curernt value of the date input to today's date
     */
    setDateValueToday = () => {
        const currentDate = DateTimeUtil.getCurrentDateIso();
        this.setDateValue(currentDate);
    }
}
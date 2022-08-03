

import { EventModalFormValues } from "../../domain/forms/event-modal-form-values";
import { EventModalSelectors } from "./event-modal-selectors";
import { Event } from "../../domain/models/event";
import { DateTimeUtil } from "../../helpers/datetime";

/**
 * This class represents all the inputs for the event modal form
 */
export class EventModalForm
{
    constructor() {
        /** @type {HTMLInputElement} */
        this.inputName            = document.getElementById(EventModalSelectors.Form.Inputs.NAME);

        /** @type {HTMLInputElement} */
        this.inputPhone           = document.getElementById(EventModalSelectors.Form.Inputs.PHONE);

        /** @type {HTMLInputElement} */
        this.inputLocation        = document.getElementById(EventModalSelectors.Form.Inputs.LOCATION);

        /** @type {HTMLInputElement} */
        this.inputStartsOn        = document.getElementById(EventModalSelectors.Form.Inputs.STARTS_ON);

        /** @type {HTMLInputElement} */
        this.inputEndsOn          = document.getElementById(EventModalSelectors.Form.Inputs.ENDS_ON);

        /** @type {HTMLInputElement} */
        this.inputStartsAt        = document.getElementById(EventModalSelectors.Form.Inputs.STARTS_AT);

        /** @type {HTMLInputElement} */
        this.inputEndsAt          = document.getElementById(EventModalSelectors.Form.Inputs.ENDS_AT);

        /** @type {HTMLInputElement} */
        this.inputFrequency       = document.getElementById(EventModalSelectors.Form.Inputs.FREQUENCY);

        /** @type {HTMLInputElement} */
        this.inputSeparation      = document.getElementById(EventModalSelectors.Form.Inputs.SEPARATION);

        /** @type {HTMLInputElement} */
        this.inputRecurrenceDay   = document.getElementById(EventModalSelectors.Form.Inputs.RECURRENCE_DAY);

        /** @type {HTMLInputElement} */
        this.inputRecurrenceWeek  = document.getElementById(EventModalSelectors.Form.Inputs.RECURRENCE_WEEK);

        /** @type {HTMLInputElement} */
        this.inputRecurrenceMonth = document.getElementById(EventModalSelectors.Form.Inputs.RECURRENCE_MONTH);
        
        /** @type {HTMLButtonElement} */
        this.submitBtn = document.getElementById(EventModalSelectors.Form.SUBMIT_BTN);

        /** @type {HTMLFormElement} */
        this.form = document.getElementById(EventModalSelectors.Form.FORM);
    }

    /**
     * Get the current input values for the form
     * @returns {EventModalFormValues}
     */
    getValues = () => {
        const values = new EventModalFormValues();

        values.name            = this.inputName.value;
        values.phoneNumber     = this.inputPhone.value;
        values.location        = this.inputLocation.value;
        values.startsOn        = this.inputStartsOn.value;
        values.endsOn          = this.inputEndsOn.value;
        values.startsAt        = this.inputStartsAt.value;
        values.endsAt          = this.inputEndsAt.value;
        values.frequency       = this.inputFrequency.value;
        values.separation      = this.inputSeparation.value;
        values.recurrenceDay   = this.inputRecurrenceDay.value;
        values.recurrenceWeek  = this.inputRecurrenceWeek.value;
        values.recurrenceMonth = this.inputRecurrenceMonth.value;

        return values;
    }

    /**
     * Set the form input values to the ones in the given event model
     * @param {Event} newEvent 
     */
    setFormValues(newEvent) {
        this.inputName.value            = newEvent.name;
        this.inputPhone.value           = newEvent.phoneNumber;
        this.inputLocation.value        = newEvent.location;
        this.inputStartsAt.value        = newEvent.startsAt;
        this.inputEndsAt.value          = newEvent.endsAt;
        this.inputFrequency.value       = newEvent.frequency;
        this.inputSeparation.value      = newEvent.separation;
        this.inputRecurrenceDay.value   = newEvent.recurrenceDay;
        this.inputRecurrenceWeek.value  = newEvent.recurrenceWeek;
        this.inputRecurrenceMonth.value = newEvent.recurrenceMonth;
        this.inputStartsOn.value        = DateTimeUtil.toDateTime(newEvent.startsOn).toISODate();
        this.inputEndsOn.value          = DateTimeUtil.toDateTime(newEvent.endsOn).toISODate();
        
    }
}

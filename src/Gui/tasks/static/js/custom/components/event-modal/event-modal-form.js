

import { EventModalFormValues } from "../../domain/forms/event-modal-form-values";
import { EventModalSelectors } from "./event-modal-selectors";

/**
 * This class represents all the inputs for the event modal form
 */
export class EventModalForm
{
    constructor() {
        this.inputName            = document.getElementById(EventModalSelectors.Form.Inputs.NAME);
        this.inputPhone           = document.getElementById(EventModalSelectors.Form.Inputs.PHONE);
        this.inputLocation        = document.getElementById(EventModalSelectors.Form.Inputs.LOCATION);
        this.inputStartsOn        = document.getElementById(EventModalSelectors.Form.Inputs.STARTS_ON);
        this.inputEndsOn          = document.getElementById(EventModalSelectors.Form.Inputs.ENDS_ON);
        this.inputStartsAt        = document.getElementById(EventModalSelectors.Form.Inputs.STARTS_AT);
        this.inputEndsAt          = document.getElementById(EventModalSelectors.Form.Inputs.ENDS_AT);
        this.inputFrequency       = document.getElementById(EventModalSelectors.Form.Inputs.FREQUENCY);
        this.inputSeparation      = document.getElementById(EventModalSelectors.Form.Inputs.SEPARATION);
        this.inputRecurrenceDay   = document.getElementById(EventModalSelectors.Form.Inputs.RECURRENCE_DAY);
        this.inputRecurrenceWeek  = document.getElementById(EventModalSelectors.Form.Inputs.RECURRENCE_WEEK);
        this.inputRecurrenceMonth = document.getElementById(EventModalSelectors.Form.Inputs.RECURRENCE_MONTH);

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
}

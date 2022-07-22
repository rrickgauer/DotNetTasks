

import { EventModalFormValues } from "../../domain/models/event-modal-form-values";

/**
 * This class represents all the inputs for the event modal form
 */
export class EventModalForm
{
    constructor() {
        this.inputName            = document.getElementById(EventModalForm.Html.Inputs.NAME);
        this.inputPhone           = document.getElementById(EventModalForm.Html.Inputs.PHONE);
        this.inputLocation        = document.getElementById(EventModalForm.Html.Inputs.LOCATION);
        this.inputStartsOn        = document.getElementById(EventModalForm.Html.Inputs.STARTS_ON);
        this.inputEndsOn          = document.getElementById(EventModalForm.Html.Inputs.ENDS_ON);
        this.inputStartsAt        = document.getElementById(EventModalForm.Html.Inputs.STARTS_AT);
        this.inputEndsAt          = document.getElementById(EventModalForm.Html.Inputs.ENDS_AT);
        this.inputFrequency       = document.getElementById(EventModalForm.Html.Inputs.FREQUENCY);
        this.inputSeparation      = document.getElementById(EventModalForm.Html.Inputs.SEPARATION);
        this.inputRecurrenceDay   = document.getElementById(EventModalForm.Html.Inputs.RECURRENCE_DAY);
        this.inputRecurrenceWeek  = document.getElementById(EventModalForm.Html.Inputs.RECURRENCE_WEEK);
        this.inputRecurrenceMonth = document.getElementById(EventModalForm.Html.Inputs.RECURRENCE_MONTH);

        this.form = document.getElementById(EventModalForm.Html.FORM);
    }

    /**
     * Get the current input values for the form
     * @returns {EventModalFormValues}
     */
    getValues = () => {
        const values = new EventModalFormValues();

        values.name            = this.inputName.value;
        values.phone           = this.inputPhone.value;
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


EventModalForm.Html = 
{
    FORM: 'event-modal-form',
    INPUTS_CLASS: 'event-modal-form-input',

    Inputs: {
        NAME            : 'event-modal-form-input-name',
        PHONE           : 'event-modal-form-input-phone',
        LOCATION        : 'event-modal-form-input-location',
        STARTS_ON       : 'event-modal-form-input-starts-on',
        ENDS_ON         : 'event-modal-form-input-ends-on',
        STARTS_AT       : 'event-modal-form-input-starts-at',
        ENDS_AT         : 'event-modal-form-input-ends-at',
        FREQUENCY       : 'event-modal-form-input-frequency',
        SEPARATION      : 'event-modal-form-input-separation',
        RECURRENCE_DAY  : 'event-modal-form-input-recurrence-day',
        RECURRENCE_WEEK : 'event-modal-form-input-recurrence-week',
        RECURRENCE_MONTH: 'event-modal-form-input-recurrence-month',
    },
}
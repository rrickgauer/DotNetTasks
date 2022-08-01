


export class EventModalSelectors {}


EventModalSelectors.CONTAINER = 'event-modal';

EventModalSelectors.Attributes = {
    EVENT_ID: 'data-js-event-id',
},

EventModalSelectors.SPINNER = 'event-modal-spinner';

EventModalSelectors.Form = 
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

    SUBMIT_BTN: 'event-modal-form-submit-btn',
}

EventModalSelectors.DeleteForm = {
    DROPDOWN: 'event-deletion-form-dropdown',
    INPUT: 'event-deletion-form-radio',
    SUBMIT_BTN: 'event-deletion-form-submit',
    FORM: 'event-deletion-form',
}
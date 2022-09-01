


export class EmailPreferencesElements
{
    constructor()
    {
        /** @type {HTMLFormElement} */
        this.eForm = document.getElementById(EmailPreferencesElements.FORM);

        /** @type {HTMLInputElement} */
        this.eDailyRemindersCheckbox = this.eForm.querySelector(`input[name='${EmailPreferencesElements.DAILY_REMINDERS_CHECKBOX}']`);

        /** @type {HTMLButtonElement} */
        this.eSubmitBtn = this.eForm.querySelector(`button[name='${EmailPreferencesElements.SUBMIT_BTN}']`);
    }
}


EmailPreferencesElements.FORM = 'email-preferences-form';
EmailPreferencesElements.DAILY_REMINDERS_CHECKBOX = 'daily-reminders-checkbox';
EmailPreferencesElements.SUBMIT_BTN = 'daily-reminders-submit';


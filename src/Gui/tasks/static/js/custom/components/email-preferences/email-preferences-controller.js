import { EmailPreferencesElements } from "./email-preferences-elements";




export class EmailPreferencesController
{
    constructor()
    {
        this.elements = new EmailPreferencesElements();
    }

    addListeners = () =>
    {
        console.log(this);

        this._listenForDailyRemindersCheckboxClick();
    }

    //#region Daily reminders

    _listenForDailyRemindersCheckboxClick = () =>
    {
        this.elements.eDailyRemindersCheckbox.addEventListener('change', (e) =>
        {
            this._updateDailyReminders();
        });
    }

    _updateDailyReminders = () =>
    {
        const isChecked = this.elements.eDailyRemindersCheckbox.checked;

        if (isChecked) {
            alert('sending you reminders now...');
        }
        else {
            alert('no more reminders');
        }
    }

    //#endregion
}
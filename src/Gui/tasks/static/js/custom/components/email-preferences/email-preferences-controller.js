import { ApiUser } from "../../api/api-user";
import { EmailPreferencesElements } from "./email-preferences-elements";




export class EmailPreferencesController
{
    constructor()
    {
        this.elements = new EmailPreferencesElements();
        this.api = new ApiUser();
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

    _updateDailyReminders = async () =>
    {
        const isChecked = this.elements.eDailyRemindersCheckbox.checked;
        
        const formData = {deliverReminders: isChecked ? 'true' : 'false'};


        console.log(formData);

        const apiResponse = await this.api.put(formData);

        console.log(await apiResponse.json());

        // if (isChecked) 
        // {
        //     alert('sending you reminders now...');
        // }
        // else 
        // {
        //     alert('no more reminders');
        // }
    }

    //#endregion
}
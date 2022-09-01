import { ApiUser } from "../../api/api-user";
import { SpinnerButton } from "../../helpers/spinner-button";
import { AlertPageTopDanger, AlertPageTopSuccess } from "../page-alerts/alert-page-top";
import { EmailPreferencesElements } from "./email-preferences-elements";

export class EmailPreferencesController
{
    constructor()
    {
        /** @type {EmailPreferencesElements} */
        this.elements = new EmailPreferencesElements();

        /** @type {ApiUser} */
        this.api = new ApiUser();

        /** @type {SpinnerButton} */
        this.spinnerButton = new SpinnerButton(this.elements.eSubmitBtn);

        /** @type {AlertPageTopSuccess} */
        this.successfulAlert = new AlertPageTopSuccess('Saved!');

        /** @type {AlertPageTopDanger} */
        this.dangerAlert = new AlertPageTopDanger('Did not save. Check console for error message.');
    }

    /**
     * Add page listeners
     */
    addListeners = () =>
    {
        this._listenForDailyRemindersCheckboxClick();
        this._listenForFormSubmission();
    }

    //#region Daily reminders checkbox change

    _listenForDailyRemindersCheckboxClick = () =>
    {
        this.elements.eDailyRemindersCheckbox.addEventListener('change', (e) =>
        {
            this._enableSubmitButton();
        });
    }

    //#endregion

    //#region Toggle the submit button
    _enableSubmitButton = () => this.elements.eSubmitBtn.disabled = false;
    _disableSubmitButton = () => this.elements.eSubmitBtn.disabled = true;
    //#endregion

    //#region Form submission

    /**
     * Listen for the form submission event
     */
    _listenForFormSubmission = () =>
    {
        this.elements.eForm.addEventListener('submit', (e) =>
        {
            e.preventDefault();
            this._updateDailyReminders();
        });
    }

    /**
     * Send the update request to the api
     */
    _updateDailyReminders = async () =>
    {
        this.spinnerButton.showSpinner();

        const isChecked = this.elements.eDailyRemindersCheckbox.checked;
        const formData = {deliverReminders: isChecked};
        const apiResponse = await this.api.put(formData);

        this.spinnerButton.reset();
        this._disableSubmitButton();

        if (apiResponse.ok)
        {
            this.successfulAlert.show();
        }
        else
        {
            this.dangerAlert.show();
            console.error(await apiResponse.text());
        }
    }

    //#endregion

    
}
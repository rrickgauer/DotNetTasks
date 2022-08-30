import { ApiPassword } from "../../api/api-password";
import { UpdatePasswordFormValidatorResults } from "../../domain/enums/update-password-form-validator-results";
import { UpdatePasswordFormValues } from "../../domain/forms/update-password-form";
import { SpinnerButton } from "../../helpers/spinner-button";
import { UpdatePasswordFormElements } from "./update-password-form-elements";
import { UpdatePasswordFormSelectors } from "./update-password-form-selectors";

export class UpdatePasswordFormController
{
    constructor()
    {
        /** @type {UpdatePasswordFormElements} */
        this.elements = new UpdatePasswordFormElements();

        /** @type {SpinnerButton} */
        this._spinnerButton = new SpinnerButton(this.elements.eSubmitBtn);

        /** @type {ApiPassword} */
        this._api = new ApiPassword();
    }

    /**
     * Add event listeners to the page
     */
    addListeners = () =>
    {
        this._listenForSubmitEvent();
        this._listenForInputChange();
    }

    //#region Form submission

    /**
     * Listen for the form submission event
     */
    _listenForSubmitEvent = () =>
    {
        this.elements.eForm.addEventListener('submit', (e) => 
        {
            this._handleFormSubmission(e);
        });
    }

    /**
     * 
     * @param {SubmitEvent} submissionEvent 
     */
    _handleFormSubmission = async (submissionEvent) =>
    {
        submissionEvent.preventDefault();

        this._setFormToLoading();

        const formValues = this._getFormValues();
        const response = await this._api.post(formValues);

        if (response.ok)
        {
            window.location.href = '/auth/login';
        }
        else
        {
            const apiResponseData = await response.json();
            this._handleInvalidUpdate(apiResponseData);
        }

        this._removeFormLoading();
    }

    /**
     * Get a new UpdatePasswordFormValues object
     * @returns {UpdatePasswordFormValues}
     */
    _getFormValues = () =>
    {
        // get the current input values
        const currentVal = this.elements.formGroupCurrent.getInputValue();
        const newVal = this.elements.formGroupNew.getInputValue();
        const confirmVal = this.elements.formGroupConfirm.getInputValue();

        const result = new UpdatePasswordFormValues(currentVal, newVal, confirmVal);

        return result;
    }


    /**
     * Disable the form inputs and show the spinner button
     */
    _setFormToLoading = () =>
    {
        this._spinnerButton.showSpinner();

        this.elements.formGroupCurrent.eInput.disabled = true;
        this.elements.formGroupNew.eInput.disabled     = true;
        this.elements.formGroupConfirm.eInput.disabled = true;
    }

    /**
     * Enable the form inputs and reset the spinner button
     */
    _removeFormLoading = () =>
    {
        this._spinnerButton.reset();

        this.elements.formGroupCurrent.eInput.disabled = false;
        this.elements.formGroupNew.eInput.disabled     = false;
        this.elements.formGroupConfirm.eInput.disabled = false;
    }

    /**
     * Handle an invalid attempt at updating the password
     * @param {Object} apiResponse the api response object
     */
    _handleInvalidUpdate = (apiResponse) =>
    {
        const errorCode = apiResponse.code;

        switch(errorCode)
        {
            case UpdatePasswordFormValidatorResults.NEW_PASSWORDS_NOT_MATCHING:
                this.elements.formGroupNew.setInvalid('');
                this.elements.formGroupConfirm.setInvalid('Passwords do not match');
                break;

            case UpdatePasswordFormValidatorResults.CURRENT_PASSWORD_INCORRECT:
                this.elements.formGroupCurrent.setInvalid('Current password is not correct');
                break;
        }

    }


    //#endregion

    //#region Password input changes

    /**
     * Remove all the invalid css classes from the form input elements whenever there is a keystroke
     */
    _listenForInputChange = () =>
    {
        const inputs = document.getElementsByClassName(UpdatePasswordFormSelectors.INPUTS_CLASS);

        for (const eInput of inputs) 
        {
            eInput.addEventListener('keydown', this._removeInputValidationDisplays);
        }
    }

    //#endregion


    /**
     * Remove all the input validation text and displays from the password inputs
     */
    _removeInputValidationDisplays = () =>
    {
        this.elements.formGroupCurrent.removeValidationClasses();
        this.elements.formGroupNew.removeValidationClasses();
        this.elements.formGroupConfirm.removeValidationClasses();
    }

}
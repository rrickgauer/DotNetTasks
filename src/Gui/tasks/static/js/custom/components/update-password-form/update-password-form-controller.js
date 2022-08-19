import { UpdatePasswordFormValidatorResults } from "../../domain/enums/update-password-form-validator-results";
import { UpdatePasswordFormElements } from "./update-password-form-elements";
import { UpdatePasswordFormSelectors } from "./update-password-form-selectors";
import { UpdatePasswordFormValidator } from "./update-password-form-validator";



export class UpdatePasswordFormController
{
    constructor()
    {
        this.elements = new UpdatePasswordFormElements();
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
        this.elements.eForm.addEventListener('submit', (e) => {
            this._handleFormSubmission(e);
        });
    }

    /**
     * 
     * @param {SubmitEvent} submissionEvent 
     */
    _handleFormSubmission = (submissionEvent) =>
    {
        submissionEvent.preventDefault();

        const validator = this._getNewPasswordValidator();
        const validationResult = validator.validate();

        switch(validationResult)
        {
            case UpdatePasswordFormValidatorResults.NEW_PASSWORDS_NOT_MATCHING:
                this._newPasswordsDontMatch();
                break;

            default:
                this._submitForm();
                break;
        }

        this.elements.formGroupCurrent.clearInputValue();
    }

    /**
     * Get a new password validator object
     * @returns {UpdatePasswordFormValidator}
     */
    _getNewPasswordValidator = () =>
    {
        // get the current input values
        const currentVal = this.elements.formGroupCurrent.getInputValue();
        const newVal = this.elements.formGroupNew.getInputValue();
        const confirmVal = this.elements.formGroupConfirm.getInputValue();

        // setup the return object
        const validator = new UpdatePasswordFormValidator(currentVal, newVal, confirmVal);

        return validator;
    }

    _newPasswordsDontMatch = () =>
    {
        // alert('new passwords do not match');

        this.elements.formGroupNew.setInvalid('');
        this.elements.formGroupConfirm.setInvalid('Passwords do not match');
    }

    _submitForm = () =>
    {
        alert('submit the form, validation passed');
    }

    //#endregion\

    //#region Password input changes

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
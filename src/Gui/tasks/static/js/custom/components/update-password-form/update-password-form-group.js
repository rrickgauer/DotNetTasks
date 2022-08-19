import { UpdatePasswordFormSelectors } from "./update-password-form-selectors";



export class UpdatePasswordFormGroup
{
    constructor(inputId)
    {
        /** @type {String} */
        this.inputId = inputId;

        /** @type {HTMLInputElement} */
        this.eInput = document.getElementById(this.inputId);

        /** @type {HTMLDivElement} */
        this.eContainer = this.eInput.closest(`.${UpdatePasswordFormGroup.CONTAINER}`);

        /** @type {HTMLDivElement} */
        this.eValidFeedback = this.eContainer.getElementsByClassName(UpdatePasswordFormSelectors.FeebackClasses.VALID)[0];
        
        /** @type {HTMLDivElement} */
        this.eInValidFeedback = this.eContainer.getElementsByClassName(UpdatePasswordFormSelectors.FeebackClasses.INVALID)[0];
    }

    
    /**
     * Get the current input value
     * @returns {String}
     */
    getInputValue = () => this.eInput.value;

    clearInputValue = () => this.eInput.value = '';


    /**
     * Set the input to an invalid state
     * @param {String} displayText text to display below the input
     */
    setInvalid = (displayText = '') =>
    {
        this.eInValidFeedback.innerText = displayText;
        this.eInput.classList.add(UpdatePasswordFormGroup.IS_INVALID);
    }

    removeValidationClasses = () => 
    {
        this.eInput.classList.remove(UpdatePasswordFormGroup.IS_INVALID);
        this.eInput.classList.remove(UpdatePasswordFormGroup.IS_VALID);
    }
}


UpdatePasswordFormGroup.CONTAINER = 'form-group';
UpdatePasswordFormGroup.IS_INVALID = 'is-invalid';
UpdatePasswordFormGroup.IS_VALID = 'is-valid';
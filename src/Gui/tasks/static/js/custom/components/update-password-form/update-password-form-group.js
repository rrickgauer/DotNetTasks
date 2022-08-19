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
}


UpdatePasswordFormGroup.CONTAINER = 'form-group';
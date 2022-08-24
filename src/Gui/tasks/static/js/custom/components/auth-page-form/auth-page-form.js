import { SpinnerButton } from "../../helpers/spinner-button";




export class AuthPageForm
{
    /**
     * Auth page form
     * @param {String} formId the form htmtl id
     */
    constructor(formId)
    {
        /** @type {String} */
        this.formId = formId;

        /** @type {HTMLFormElement} */
        this.eForm = document.getElementById(this.formId);

        /** @type {HTMLInputElement} */
        this.eInputEmail = this.eForm.querySelector(`input[type="email"]`);

        /** @type {HTMLInputElement} */
        this.eInputPassword = this.eForm.querySelector(`input[type="password"]`);

        /** @type {HTMLButtonElement} */
        this.eSubmitBtn = this.eForm.querySelector(`button[type="submit"]`);

        /** @type {SpinnerButton} */
        this.spinnerBtn = new SpinnerButton(this.eSubmitBtn);
    }
}


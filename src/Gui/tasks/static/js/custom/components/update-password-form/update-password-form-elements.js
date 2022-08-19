import { UpdatePasswordFormGroup } from "./update-password-form-group";
import { UpdatePasswordFormSelectors } from "./update-password-form-selectors";



export class UpdatePasswordFormElements
{
    constructor()
    {
        /** @type {UpdatePasswordFormGroup} */
        this.formGroupCurrent = new UpdatePasswordFormGroup(UpdatePasswordFormSelectors.Inputs.CURRENT);

        /** @type {UpdatePasswordFormGroup} */
        this.formGroupNew = new UpdatePasswordFormGroup(UpdatePasswordFormSelectors.Inputs.NEW);

        /** @type {UpdatePasswordFormGroup} */
        this.formGroupConfirm = new UpdatePasswordFormGroup(UpdatePasswordFormSelectors.Inputs.CONFIRM);

        /** @type {HTMLButtonElement} */
        this.eSubmitBtn = document.getElementById(UpdatePasswordFormSelectors.SUBMIT_BTN);

        /** @type {HTMLFormElement} */
        this.eForm = document.getElementById(UpdatePasswordFormSelectors.FORM);

    }
}
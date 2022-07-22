
import { Utililties } from "../../helpers/utilities";


export class LoginPageForm
{
    constructor()
    {
        this.form      = document.getElementById(LoginPageForm.Html.FORM);
        this.email     = document.getElementById(LoginPageForm.Html.Inputs.EMAIL);
        this.password  = document.getElementById(LoginPageForm.Html.Inputs.PASSWORD);
        this.submitBtn = document.getElementById(LoginPageForm.Html.Buttons.SUBMIT);
    }

    getEmailValue    = () => this.email.value;
    getPasswordValue = () => this.password.value;

    /**
     * Validates the form.
     * @returns {Boolean}
     */
    isValid = () => {
        return Utililties.validateForm(this.form);
    }



}


LoginPageForm.Html = {
    
    FORM: 'login-form',
    
    
    Inputs: {
        EMAIL: 'login-form-input-email',
        PASSWORD: 'login-form-input-password',
    },



    Buttons: {
        SUBMIT: 'login-form-btn-submit',
    }
}

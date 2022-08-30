import { ApiEmailVerifications } from "../../api/api-email-verifications";
import { SpinnerButton } from "../../helpers/spinner-button";
import { AlertPageTopDanger, AlertPageTopSuccess } from "../page-alerts/alert-page-top";
import { VerifyAccountElements } from "./verrify-account-elements"


export class VerifyAccountController
{
    constructor()
    {
        this.elements = new VerifyAccountElements();
        this.spinnerBtn = new SpinnerButton(this.elements.eSubmitBtn);
        this.api = new ApiEmailVerifications();
    }

    addListeners = () =>
    {
        this._listenForSendEmailVerificationBtnClick();
    }

    //#region Send email verification

    _listenForSendEmailVerificationBtnClick = () =>
    {
        this.elements.eSubmitBtn.addEventListener('click', (e) =>
        {
            this._sendEmailVerification();
        });
    }

    _sendEmailVerification = async () =>
    {
        this.spinnerBtn.showSpinner();

        const response = await this.api.post();

        this.spinnerBtn.reset();

        if (response.ok)
        {
            this._sendEmailVerificationSuccess();
        }
        else
        {
            await this._sendEmailVerificationError(response);
        }      

        
    }

    /**
     * Handle an invalid email verification request
     * @param {Promise<Response>} response the api response
     */
    _sendEmailVerificationError = async (response) =>
    {
        console.error(await response.text());

        const alertTop = new AlertPageTopDanger('There was an error sending the email. Check console.');
        alertTop.show();
    }

    /**
     * Handle a successful verification request
     */
    _sendEmailVerificationSuccess = () =>
    {
        const alertTop = new AlertPageTopSuccess('Success! Please check your email for the confirmation.');
        alertTop.show();

        this.elements.eSubmitBtn.disabled = true;
        // this.elements.eSubmitBtn.classList.add('disabled');
    }

    //#endregion
}


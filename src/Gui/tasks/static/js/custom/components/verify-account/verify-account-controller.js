import { ApiEmailVerifications } from "../../api/api-email-verifications";
import { SpinnerButton } from "../../helpers/spinner-button";
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

    _listenForSendEmailVerificationBtnClick = () =>
    {
        this.elements.eSubmitBtn.addEventListener('click', (e) =>
        {
            this.sendEmailVerification();
        });
    }

    sendEmailVerification = async () =>
    {
        this.spinnerBtn.showSpinner();

        const response = await this.api.post();

        this.spinnerBtn.reset();

        console.log(await response.text());
    }
}


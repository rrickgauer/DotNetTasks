
import { EmailPreferencesController } from "../../components/email-preferences/email-preferences-controller";
import { UpdatePasswordFormController } from "../../components/update-password-form/update-password-form-controller";
import { VerifyAccountController } from "../../components/verify-account/verify-account-controller";

const m_updatePasswordFormController = new UpdatePasswordFormController();
const m_verifyAccountController = new VerifyAccountController();
const m_emailPreferencesController = new EmailPreferencesController();

/**
 * Main logic
 */
$(document).ready(function() 
{
    addListeners();
});



function addListeners() 
{
    m_updatePasswordFormController.addListeners();
    m_verifyAccountController.addListeners();
    m_emailPreferencesController.addListeners();
}
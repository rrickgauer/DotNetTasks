import { UpdatePasswordFormController } from "../../components/update-password-form/update-password-form-controller";



const m_updatePasswordFormController = new UpdatePasswordFormController();


/**
 * Main logic
 */
$(document).ready(function() 
{
    addListeners();
});



function addListeners() 
{
    console.log(m_updatePasswordFormController);
}
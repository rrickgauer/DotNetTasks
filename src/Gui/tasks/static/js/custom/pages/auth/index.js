

import { ApiLogin } from "../../api/api-login";
import { AuthPageForm } from "../../components/auth-page-form/auth-page-form";
import { AuthPageFormElements } from "../../components/auth-page-form/auth-page-form-elements";

const m_formLogin = new AuthPageForm(AuthPageFormElements.FORM_LOGIN);
const m_formSignUp = new AuthPageForm(AuthPageFormElements.FORM_SIGNUP);

/**
 * Main logic
 */
$(document).ready(function() {
    addListeners();
});


/** 
 * Add the event listeners to the page 
 */
function addListeners() {
    // log in attempt
    m_formLogin.eForm.addEventListener('submit', (formSubmitEvent) => 
    {
        formSubmitEvent.preventDefault();
        loginUser();
    });
}

//#region Log in

/**
 * Log the user in.
 * If the login credentials were valid, redirect the user to the home page.
 */
 async function loginUser() {
    m_formLogin.spinnerBtn.showSpinner();

    const successfulLogin = await sendLoginRequest();

    m_formLogin.spinnerBtn.reset();

    if (!successfulLogin) {    
        alert('Invalid login attempt');
        return;
    }

    // redirect to home page
    window.location.href = '/app';
}


/**
 * Runs through the steps of a login attempt.
 * @returns {Promise<Boolean>}
 */
async function sendLoginRequest() {
    const api = new ApiLogin(m_formLogin.eInputEmail.value, m_formLogin.eInputPassword.value);

    const response = await api.login();

    if (!response.ok)
    {
        console.error(await response.text());
    }

    return response.ok;
}

//#endregion




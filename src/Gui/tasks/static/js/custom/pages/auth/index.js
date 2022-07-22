

import { LoginPageForm } from "./page-elements";
import { ApiLogin } from "../../api/api-login";

let m_loginForm = new LoginPageForm();


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

    // login form submission 
    $(m_loginForm.form).on('submit', function(event) {
        loginUser(event);
    });
}

/**
 * Log the user in.
 * 
 * If the login credentials were valid, redirect the user to the home page.
 * 
 * @param {Event} formSubmitEvent - event raised from the login form submission
 */
async function loginUser(formSubmitEvent) {
    formSubmitEvent.preventDefault();

    const successfulLogin = await attemptToLogin();

    if (!successfulLogin) {
        alert('Invalid login attempt');
        return;
    }

    // redirect to home page
    window.location.href = '/app';
}

/**
 * Runs through the steps of a login attempt.
 * @returns {Boolean}
 */
async function attemptToLogin() {
    if (!m_loginForm.isValid()) {
        return false;
    }

    const api = new ApiLogin(m_loginForm.getEmailValue(), m_loginForm.getPasswordValue());
    const apiResponse = await api.login();
    return apiResponse.ok;
}





import { LoginPageForm } from "./page-elements";

let m_loginForm = new LoginPageForm();


/**
 * Main logic
 */
$(document).ready(function() {
    // m_loginForm = LoginPageForm();
    addListeners();
});

/** Add the event listeners to the page */
function addListeners() {
    $(m_loginForm.submitBtn).on('click', loginUser);
}

/** Log the user in */
async function loginUser() {
    const successfulLogin = attemptToLogin();

    if (!successfulLogin) {
        return;
    }

    // redirect to home page
}

/**
 * Runs through the steps of a login attempt.
 * @returns {Boolean}
 */
async function attemptToLogin() {
    if (!m_loginForm.isValid()) {
        return false;
    }


    return true;

}



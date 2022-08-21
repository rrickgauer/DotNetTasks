import { UpdatePasswordFormValidatorResults } from "../../domain/enums/update-password-form-validator-results";



export class UpdatePasswordFormValidator
{
    /**
     * Validator for the update password form.
     * @param {String} currentValue current password
     * @param {String} newValue new password
     * @param {String} confirmValue confirm new password value
     */
    constructor(currentValue, newValue, confirmValue)
    {
        /** @type {String} */
        this.current = currentValue;
        
        /** @type {String} */
        this.new     = newValue;

        /** @type {String} */
        this.confirm = confirmValue;
    }
    

    /**
     * Validates the password values
     * @returns {UpdatePasswordFormValidatorResults}
     */
    validate = () =>
    {
        if (!this._doNewPasswordsMatch())
        {
            return UpdatePasswordFormValidatorResults.NEW_PASSWORDS_NOT_MATCHING;
        }

        return UpdatePasswordFormValidatorResults.VALID;
    }


    /**
     * Checks if the new password values match
     * @returns {Boolean}
     */
    _doNewPasswordsMatch = () =>
    {
        if (this.new != this.confirm)
        {
            return false;
        }

        return true;
    }


    _currentPasswordIsCorrect = async () =>
    {
        
    }

}
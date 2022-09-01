

export class Utililties
{

    /**
     * Validates the form attributes.
     * @param {HTMLFormElement} formElement - the form to validate
     * @returns true if form is valid, otherwise false
     */
    static validateForm(formElement) {
        return $(formElement)[0].reportValidity();
    }

    /**
     * Generate a new UUID
     * @returns {string} the uuid
     */
    static getNewUUID() {
        return uuidv4();
    }

    
    static isNullOrEmpty(val) 
    {

        if (val === undefined) 
        {
            return true;
        }
        else if (val === null) 
        {
            return true;
        }
        else if (val === "") 
        {
            return true;
        }
        else if (val.length === 0) 
        {
            return true;
        }
        
        return false;
    }

    static isTypeOf(value, type)
    {
        const valueType = typeof value;

        const typeType = typeof type;

        return valueType === typeType;
    }

}
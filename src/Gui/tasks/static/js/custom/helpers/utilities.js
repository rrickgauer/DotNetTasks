

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
     * @returns {string}
     */
    static getNewUUID() {
        return uuidv4();
    }

    /**
     * Map the specified object to a FormData object
     * @param {Object} canidateObject - the object to map
     * @returns {FormData} the form data
     */
    static toFormData(canidateObject) {
        // const formData = new FormData();
        const formData = new URLSearchParams();

        for (const key in canidateObject) {
            formData.append(key, canidateObject[key]);
        }

        return formData;
    }

}
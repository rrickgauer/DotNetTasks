
import { Utililties } from "../helpers/utilities";

export class HttpRequestMapper {
    /**
     * Map the specified object to a FormData object
     * @param {Object} data - the object to map
     * @returns {FormData} the form data
     */
    static toFormData(data) {
        const formData = new FormData();

        for (const key in data) {
            let value = data[key];

            if (Utililties.isNullOrEmpty(value)) 
            {
                continue;    
            }
            
            if (Utililties.isTypeOf(value, false))
            {
                value = value ? 'true' : 'false';
            }

            formData.append(key, value);
        }

        return formData;
    }
}

import { Utililties } from "../helpers/utilities";

export class HttpRequestMapper
{
    /**
     * Map the specified object to a FormData object
     * @param {Object} data - the object to map
     * @returns {FormData} the form data
     */
         static toFormData(data) {
            const formData = new FormData();
    
            for (const key in data) {
                const val = data[key];
    
                if (!Utililties.isNullOrEmpty(val)) {
                    formData.append(key, val);
                }
            }
            
            return formData;
        }
}
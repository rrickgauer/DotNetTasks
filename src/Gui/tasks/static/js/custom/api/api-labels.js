import { ApiEndpoints } from "./api-base";
import { HttpMethods } from "./api-base";
import { HttpRequestMapper } from "../mappers/http-request-mapper";


export class ApiLabels
{

    /**
     * Get the labels html
     * @returns {Promise<Response>}
     */
    getAll = async () => 
    {
        return await fetch(ApiEndpoints.LABELS);
    }

    /**
     * Get a label from the api
     * @param {String} labelId the label id
     * @returns {Promise<Response>}
     */
    get = async (labelId) =>
    {
        const url = `${ApiEndpoints.LABELS}/${labelId}`;

        return await fetch(url);
    }
}


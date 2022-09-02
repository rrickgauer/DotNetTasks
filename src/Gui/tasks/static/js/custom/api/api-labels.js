import { ApiEndpoints } from "./api-base";
import { HttpMethods } from "./api-base";
import { HttpRequestMapper } from "../mappers/http-request-mapper";


export class ApiLabels
{

    /**
     * Get the labels html
     * @returns {Promise<Response>}
     */
    get = async () => 
    {
        return await fetch(ApiEndpoints.LABELS);
    }
}


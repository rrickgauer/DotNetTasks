
import { ApiEndpoints, HttpMethods } from "./api-base";

export class ApiEmailVerifications
{
    /**
     * Send post request
     * @returns {Promise<Response>}
     */
    post = async () =>
    {
        const url = ApiEndpoints.EMAIL_VERIFICATIONS;

        return await fetch(url, {
            method: HttpMethods.POST,
        })
    }
}


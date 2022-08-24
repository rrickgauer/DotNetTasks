import { ApiEndpoints } from "./api-base";
import { HttpMethods } from "./api-base";
import { HttpRequestMapper } from "../mappers/http-request-mapper";


export class ApiUser
{
    /**
     * Send a sign up request to the api
     * @param {String} email email value
     * @param {String} password password value
     * @returns {Promise<Response>}
     */
    signUp = async (email, password) => 
    {
        const data = HttpRequestMapper.toFormData({
            email: email,
            password: password,
        });

        const url = `${ApiEndpoints.USER}/signup`;

        return await fetch(url, {
            body: data,
            method: HttpMethods.POST,
        });
    }
}


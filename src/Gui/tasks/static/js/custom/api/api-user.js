import { ApiEndpoints } from "./api-base";
import { HttpMethods } from "./api-base";
import { HttpRequestMapper } from "../mappers/http-request-mapper";


export class ApiUser
{
    /**
     * Send a sign up request to the api (POST)
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

    /**
     * Send a put request to update the user
     * @param {Object} formData the request body to send
     * @returns {Promise<Response>}
     */
    put = async (formData) =>
    {
        const data = HttpRequestMapper.toFormData(formData);


        console.log(data);

        const sssss = Array.from(data.entries());
        console.log(sssss);


        const url = `${ApiEndpoints.USER}`;

        return await fetch(url, {
            method: HttpMethods.PUT,
            body: data,
        });
    }
}


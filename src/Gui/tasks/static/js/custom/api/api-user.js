import { ApiEndpoints } from "./api-base";
import { HttpMethods } from "./api-base";
import { HttpRequestMapper } from "../mappers/http-request-mapper";
import { UpdatePasswordArgs } from "../domain/models/update-password-args";


export class ApiUser
{

    /**
     * 
     * @param {UpdatePasswordArgs} passwords password args
     * @returns {Promise<Response>} the response
     */
    updatePassword = async (passwords) =>
    {
        const url = ApiEndpoints.USER;
        const data = HttpRequestMapper.toFormData(passwords);

        return await fetch(url, {
            method: HttpMethods.PATCH,
            body: data,
        });
    }

    
    isCurrentPassword = async (password) =>
    {
        const url = ApiEndpoints.USER;
        const data = HttpRequestMapper.toFormData(passwords);

        return await fetch(url, {
            method: HttpMethods.GET,
            body: data,
        });
    }

}


import { ApiEndpoints } from "./api-base";
import { HttpMethods } from "./api-base";
import { HttpRequestMapper } from "../mappers/http-request-mapper";
import { UpdatePasswordFormValues } from "../domain/forms/update-password-form";


export class ApiPassword
{

    /**
     * Update the user password
     * @param {UpdatePasswordFormValues} updatePasswordFormValues new password values
     * @returns {Promise<Response>}
     */
    post = async (updatePasswordFormValues) => {
        const data = HttpRequestMapper.toFormData(updatePasswordFormValues);

        return await fetch(ApiEndpoints.PASSWORD, {
            method: HttpMethods.POST,
            body: data,
        });
    }
}


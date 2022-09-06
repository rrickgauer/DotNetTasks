import { ApiEndpoints } from "./api-base";
import { HttpMethods } from "./api-base";
import { HttpRequestMapper } from "../mappers/http-request-mapper";
import { ModifyLabelForm } from "../domain/forms/modify-label-form";


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


    /**
     * Send a put request
     * @param {String} labelId the label id
     * @param {ModifyLabelForm} formValues the label form values
     * @returns {Promise<Response>}
     */
    put = async (labelId, formValues) =>
    {
        const url = `${ApiEndpoints.LABELS}/${labelId}`;
        const data = HttpRequestMapper.toFormData(formValues);

        return await fetch(url, {
            method: HttpMethods.PUT,
            body: data,
        });
    }
}


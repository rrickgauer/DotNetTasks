import { ApiEndpoints, HttpMethods } from "./api-base";



export class ApiChecklists
{

    getAll = async () =>
    {
        return await fetch(ApiEndpoints.CHECKLISTS);
    }

    post = async(checklistForm) =>
    {
        const url = ApiEndpoints.CHECKLISTS;

        return await fetch(url, {
            method: HttpMethods.POST,
            body: checklistForm,
        });
    }


    get = async (checklistId) =>
    {
        const url = this.#getUrl(checklistId);
        return await fetch(url);
    }


    delete = async (checklistId) =>
    {
        const url = this.#getUrl(checklistId);
        
        return await fetch(url, 
        {
            method: HttpMethods.DELETE,
        });
    }


    /**
     * Send a put request
     * @param {string} checklistId 
     * @param {FormData} formData 
     * @returns 
     */
    put = async (checklistId, formData) =>
    {
        const url = this.#getUrl(checklistId);

        return await fetch(url, {
            method: HttpMethods.PUT,
            body: formData,
        });
    }


    #getUrl = (checklistId) =>
    {
        return `${ApiEndpoints.CHECKLISTS}/${checklistId}`;
    }

}
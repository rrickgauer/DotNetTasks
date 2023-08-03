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

    #getUrl = (checklistId) =>
    {
        return `${ApiEndpoints.CHECKLISTS}/${checklistId}`;
    }

}
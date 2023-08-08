import { ApiEndpoints, HttpMethods } from "./api-base";


export class ApiChecklistItems
{
    constructor(checklistId) 
    {
        this.checklistId = checklistId;
    }

    get url()
    {
        return `${ApiEndpoints.CHECKLISTS}/${this.checklistId}/items`;
    }

    getAll = async () =>
    {
        return await fetch(this.url);
    }

    /**
     * Send POST request
     * @param {FormData} form the form data
     * @returns the response
     */
    post = async (form) =>
    {
        return await fetch(this.url, {
            method: HttpMethods.POST,
            body: form,
        });
    }

    delete = async (checklistItemId) =>
    {
        const url = `${this.url}/${checklistItemId}`;

        return await fetch(url, {
            method: HttpMethods.DELETE,
        });
    }
}
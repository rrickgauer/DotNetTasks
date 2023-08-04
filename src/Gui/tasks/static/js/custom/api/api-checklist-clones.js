import { ApiEndpoints, HttpMethods } from "./api-base";


export class ApiChecklistClones
{
    constructor(checklistId) 
    {
        this.checklistId = checklistId;
    }

    get url()
    {
        return `${ApiEndpoints.CHECKLISTS}/${this.checklistId}/clones`;
    }


    /**
     * Post
     * @param {FormData} formData the form data
     * @returns the api response   
     */
    post = async (formData) =>
    {
        const url = this.url;

        return await fetch(url, {
            method: HttpMethods.POST,
            body: formData,
        });
    }
}
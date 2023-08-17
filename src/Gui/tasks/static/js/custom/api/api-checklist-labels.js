import { ApiEndpoints, HttpMethods } from "./api-base";


export class ApiChecklistLabels
{
    constructor(checklistId) 
    {
        this.checklistId = checklistId;
    }

    get url()
    {
        return `${ApiEndpoints.CHECKLISTS}/${this.checklistId}/labels`;
    }


    delete = async (labelId) =>
    {
        const url = `${this.url}/${labelId}`;

        return await fetch(url, {
            method: HttpMethods.DELETE,
        });
    }


    put = async (labelId) =>
    {   
        const url = `${this.url}/${labelId}`;

        return await fetch(url, {
            method: HttpMethods.PUT,
        });
    }
}
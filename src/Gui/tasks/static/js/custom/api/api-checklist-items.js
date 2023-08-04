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
}
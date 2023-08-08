import { ApiEndpoints, HttpMethods } from "./api-base";


export class ApiChecklistItemComplete
{
    constructor(checklistId, checklistItemId) 
    {
        this.checklistId = checklistId;
        this.checklistItemId = checklistItemId;
    }

    get url()
    {
        return `${ApiEndpoints.CHECKLISTS}/${this.checklistId}/items/${this.checklistItemId}/complete`;
    }

    put = async () =>
    {
        return await fetch(this.url, {
            method: HttpMethods.PUT,
        });
    }

    delete = async () =>
    {
        return await fetch(this.url, {
            method: HttpMethods.DELETE,
        });
    }
}
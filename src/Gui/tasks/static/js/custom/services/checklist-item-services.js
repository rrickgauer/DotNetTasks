import { ApiChecklistItems } from "../api/api-checklist-items";
import { ServiceUtilities } from "./service-utilities";




export class ChecklistItemServices
{
    constructor(checklistId)
    {
        /** @type {string} */
        this.checklistId = checklistId;

        this.api = new ApiChecklistItems(this.checklistId);
    }


    getChecklistItemsHtml = async () =>
    {
        const response = await this.api.getAll();
        await ServiceUtilities.handleBadResponse(response);
        return response.text();
    }
}
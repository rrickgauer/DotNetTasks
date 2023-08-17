"use strict"

import { ApiChecklistLabels } from "../api/api-checklist-labels";
import { ServiceUtilities } from "./service-utilities";

export class ChecklistLabelServices
{
    /** @type {string} */
    #checklistId;
    
    /** @type {ApiChecklistLabels} */
    #api;

    constructor(checklistId)
    {
        this.#checklistId = checklistId;
        this.#api = new ApiChecklistLabels(this.#checklistId);
    }


    assignLabel = async (labelId) =>
    {
        const response = await this.#api.put(labelId);
        await ServiceUtilities.handleBadResponse(response);
        return await response.json();
    }

    deleteAssignment = async (labelId) =>
    {
        const response = await this.#api.delete(labelId);
        await ServiceUtilities.handleBadResponse(response);
    }

    getAssignedLabelsHtml = async () =>
    {
        const response = await this.#api.getAll();
        
        await ServiceUtilities.handleBadResponse(response);

        return await response.text();
    }
}
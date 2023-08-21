import { NativeEvents } from "../../../domain/constants/native-events";
import { ChecklistSettingsChecklistDeletedEvent } from "../../../domain/events/events";
import { ChecklistServices } from "../../../services/checklist-services";


export class DeleteChecklistElements
{
    constructor()
    {
        /** @type {HTMLButtonElement} */
        this.deleteChecklistButton = document.querySelector('.btn-delete-checklist');
    }
}


export class DeleteChecklistController
{

    constructor(checklistId)
    {
        this.checklistId = checklistId;
        this.pageElements = new DeleteChecklistElements();
        this.checklistServices = new ChecklistServices();
    }

    init = () =>
    {
        this.#addEventListeners();
    }

    #addEventListeners = () =>
    {
        this.pageElements.deleteChecklistButton.addEventListener(NativeEvents.Click, this.#deleteChecklist);
    }
    

    #deleteChecklist = async () =>
    {
        if (!confirm('Are you sure you want to delete this checklist? This cannot be undone.'))
        {
            return;
        }

        try
        {
            await this.checklistServices.deleteChecklist(this.checklistId);
            ChecklistSettingsChecklistDeletedEvent.invoke(this);
        }
        catch(error)
        {
            alert('Could not delete the checklist.');
            return;
        }

    }

}
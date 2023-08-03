import { OpenChecklistCloseButtonClickedEvent } from "../../domain/events/events";
import { OpenChecklist } from "./open-checklist";




export class OpenChecklistsElements
{
    /** @type {HTMLDivElement} */
    container = document.querySelector('.open-checklists-wrapper');


}



export class OpenChecklistsController
{

    constructor()
    {
        this.elements = new OpenChecklistsElements();

        /** @type {OpenChecklist[]} */
        this.openChecklists = [];
    }


    init = async () =>
    {
        this.#addEventListeners();
    }


    #addEventListeners = () =>
    {

    }


    openChecklist = async (checklistId) =>
    {
        const openChecklist = await this.#createNewOpenChecklist(checklistId);
        this.openChecklists.push(openChecklist);
    }

    #createNewOpenChecklist = async (checklistId) =>
    {
        const openChecklist = new OpenChecklist(checklistId);
        await openChecklist.fetchData();
        
        if (openChecklist.isLoaded)
        {
            openChecklist.appendChecklistToContainer(this.elements.container);
        }

        return openChecklist;
    }


    closeOpenChecklist = (checklistId) =>
    {
        // remove the html from the page
        const openChecklist = this.#getOpenChecklist(checklistId);
        openChecklist.close();

        // remove the object from the collection
        this.openChecklists = this.openChecklists.filter(c => c.checklistId !== checklistId);
    }


    #getOpenChecklist = (checklistId) =>
    {
        const index = this.#getOpenChecklistIndex(checklistId);
        return this.openChecklists[index];
    }


    #getOpenChecklistIndex = (checklistId) =>
    {
        const index = this.openChecklists.findIndex(c => c.checklistId === checklistId);

        if (index == -1)
        {
            throw new Error(`There is no open checklist with this id: ${checklistId}`);
        }

        return index;
    }


}







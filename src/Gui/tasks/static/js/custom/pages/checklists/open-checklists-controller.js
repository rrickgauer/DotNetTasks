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
        const openChecklist = new OpenChecklist(checklistId);
        await openChecklist.fetchData();

        $(this.elements.container).append(openChecklist.html);
    }


}
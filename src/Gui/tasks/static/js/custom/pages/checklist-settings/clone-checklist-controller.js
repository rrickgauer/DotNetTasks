


export class CloneChecklistElements
{
    constructor()
    {

    }
}




export class CloneChecklistController
{
    constructor(checklistId)
    {
        this.checklistId = checklistId;
        this.elements = new CloneChecklistElements();
    }


    init = () =>
    {
        this.#addEventListeners();
    }

    #addEventListeners = () =>
    {
        
    }

}
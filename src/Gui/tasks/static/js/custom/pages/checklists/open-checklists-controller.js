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


    /**
     * Initialize the open checklists
     * @param {string[]} openChecklistIds list of all open checklist ids to load
     */
    init = async (openChecklistIds) =>
    {
        await this.#openChecklists(openChecklistIds);
    }


    /**
     * Open mulitple checklists
     * @param {string[]} checklistIds list of checklist ids to open
     */
    #openChecklists = async (checklistIds) =>
    {
        const promises = Array.from(checklistIds, (id) => this.openChecklist(id));
        await Promise.all(promises);
    }


    /**
     * Open the specified checklist
     * @param {string} checklistId 
     */
    openChecklist = async (checklistId) =>
    {
        const openChecklist = await this.#createNewOpenChecklist(checklistId);
        this.openChecklists.push(openChecklist);

        const checklist = this.#getOpenChecklist(checklistId);

        try 
        {
            checklist.showChecklistItemsSpinner();
            await checklist.fetchChecklistData();
        }
        catch(error)
        {
            alert(error.message);
        }
        finally
        {
            checklist.hideChecklistItemsSpinner();
        }
        
    }


    /**
     * Create and load a checklist
     * @param {string} checklistId the id of the checklist
     * @returns the created checklist 
     */
    #createNewOpenChecklist = async (checklistId) =>
    {
        const openChecklist = new OpenChecklist(checklistId);
        await openChecklist.fetchMetaData();
        
        if (openChecklist.isMetaDataLoaded)
        {
            openChecklist.appendChecklistToContainer(this.elements.container);
        }

        return openChecklist;
    }



    /**
     * Close an open checklist
     * @param {string} checklistId the checklist to close
     */
    closeOpenChecklist = (checklistId) =>
    {
        // remove the html from the page
        const openChecklist = this.#getOpenChecklist(checklistId);
        openChecklist.close();

        // remove the object from the collection
        this.openChecklists = this.openChecklists.filter(c => c.checklistId !== checklistId);
    }


    /**
     * Get the specified checklist
     * @param {string} checklistId 
     */
    #getOpenChecklist = (checklistId) =>
    {
        const index = this.#getOpenChecklistIndex(checklistId);
        return this.openChecklists[index];
    }


    /**
     * Get the index of the specified checklist within the objects openChecklists collection
     * @param {string} checklistId 
     */
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







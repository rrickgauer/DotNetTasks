import { NativeEvents } from "../../domain/constants/native-events";
import { DeleteChecklistEvent, OpenChecklistCloseButtonClickedEvent } from "../../domain/events/events";
import { ChecklistServices } from "../../services/checklist-services";


class ActionButtons
{
    static EDIT = "edit";
    static DELETE = "delete";
}


export class OpenChecklistElements 
{
    static ContainerClass = 'open-checklist-container';
    static ChecklistIdAttribute = 'data-checklist-id';
    static ActionButtonAttribute = 'data-js-action';

    static getSelector = (checklistId) =>
    {
        const result = `.${OpenChecklistElements.ContainerClass}[${OpenChecklistElements.ChecklistIdAttribute}="${checklistId}"]`;
        return result;
    }

    constructor(checklistId)
    {
        /** @type {string} */
        this.checklistId = checklistId;
        
        /** @type {HTMLDivElement} */
        this.container = this.#getContainer();

        /** @type {HTMLButtonElement} */
        this.closeButton = this.container.querySelector('.close-checklist-btn');

        /** @type {HTMLDivElement} */
        this.actionButtonsDropdown = this.container.querySelector('.action-buttons');

        /** @type {HTMLButtonElement} */
        this.deleteButton = this.actionButtonsDropdown.querySelector(`.dropdown-item[${OpenChecklistElements.ActionButtonAttribute}=${ActionButtons.DELETE}]`);
    }


    #getContainer = () =>
    {
        const containerSelectorText = OpenChecklistElements.getSelector(this.checklistId);
        return document.querySelector(containerSelectorText);
    }
    
}



export class OpenChecklist
{

    #isLoaded = false;
    get isLoaded()
    {
        return this.#isLoaded;
    }

    constructor(checklistId)
    {
        this.checklistId = checklistId;
        this.services = new ChecklistServices();
        this.html = null;

        /** @type {OpenChecklistElements} */
        this.elements = null;
    }


    close = () =>
    {
        this.elements.container.remove();
    }


    /**
     * Fetch the metadata for the checklist
     */
    fetchData = async () =>
    {
        this.html = await this.services.getChecklistHtml(this.checklistId);
        this.#isLoaded = true;
    }

    /**
     * Append this checklist to the container
     * @param {HTMLElement} container the container to add this checklist to
     */
    appendChecklistToContainer = (container) =>
    {
        $(container).append(this.html);
        this.elements = new OpenChecklistElements(this.checklistId);
        this.#addListeners();
    }

    #addListeners = () =>
    {
        this.elements.closeButton.addEventListener(NativeEvents.CLICK, (e) => 
        {
            OpenChecklistCloseButtonClickedEvent.invoke(this, this.checklistId);
        });

        this.elements.deleteButton.addEventListener(NativeEvents.CLICK, (e) =>
        {
            DeleteChecklistEvent.invoke(this, this.checklistId);
        });
    }





    



    
}


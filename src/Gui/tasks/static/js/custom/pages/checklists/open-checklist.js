import { NativeEvents } from "../../domain/constants/native-events";
import { DeleteChecklistEvent, OpenChecklistCloseButtonClickedEvent } from "../../domain/events/events";
import { ChecklistItemServices } from "../../services/checklist-item-services";
import { ChecklistServices } from "../../services/checklist-services";
import { OpenChecklistItem, OpenChecklistItemElements } from "./checklist-item";


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
    static ChecklistItemsLoadingClass = 'loading';

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

        /** @type {HTMLDivElement} */
        this.checklistItemsBody = this.container.querySelector('.open-checklist-body');

        /** @type {HTMLDivElement} */
        this.itemsContainer = this.container.querySelector('.open-checklist-body-items-container');
    }


    #getContainer = () =>
    {
        const containerSelectorText = OpenChecklistElements.getSelector(this.checklistId);
        return document.querySelector(containerSelectorText);
    }
    
}



export class OpenChecklist
{

    #isMetaDataLoaded = false;
    get isMetaDataLoaded()
    {
        return this.#isMetaDataLoaded;
    }


    constructor(checklistId)
    {
        this.checklistId = checklistId;
        this.checklistServices = new ChecklistServices();
        this.checklistItemServices = new ChecklistItemServices(this.checklistId);


        this.html = null;

        /** @type {OpenChecklistElements} */
        this.elements = null;

        /** @type {OpenChecklistItem[]} */
        this.checklistItems = [];
    }


    close = () =>
    {
        this.elements.container.remove();
    }


    /**
     * Fetch the metadata for the checklist
     */
    fetchMetaData = async () =>
    {
        this.html = await this.checklistServices.getChecklistHtml(this.checklistId);
        this.#isMetaDataLoaded = true;
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



    fetchItems = async () =>
    {
        const checklistItemsHtml = await this.checklistItemServices.getChecklistItemsHtml();
        this.elements.itemsContainer.innerHTML = checklistItemsHtml;

        this.#initOpenChecklistItemsFromHtml();
    }


    #initOpenChecklistItemsFromHtml = () =>
    {
        this.checklistItems = [];
        
        const selector = `.${OpenChecklistItemElements.ContainerClass}`;
        const itemsHtml = this.elements.itemsContainer.querySelectorAll(selector);

        for (const itemHtmlElement of itemsHtml)
        {
            const openChecklistItem = new OpenChecklistItem(itemHtmlElement, this.checklistId);
            this.checklistItems.push(openChecklistItem);
        }
    }


    showChecklistItemsSpinner = () =>
    {
        this.elements.checklistItemsBody.classList.add(OpenChecklistElements.ChecklistItemsLoadingClass);
    }

    hideChecklistItemsSpinner = () =>
    {
        this.elements.checklistItemsBody.classList.remove(OpenChecklistElements.ChecklistItemsLoadingClass);
    }

    
}


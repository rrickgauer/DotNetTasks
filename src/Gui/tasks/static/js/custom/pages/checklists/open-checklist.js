import { NativeEvents } from "../../domain/constants/native-events";
import { ChecklistItemDeleteButtonClickedEvent, DeleteChecklistEvent, OpenChecklistCloseButtonClickedEvent } from "../../domain/events/events";
import { CreateChecklistItemForm } from "../../domain/forms/checklist-item-forms";
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

        /** @type {HTMLDivElement} */
        this.newItemFormContainer = this.container.querySelector('.open-checklist-body-new-item-form-container');
        
        /** @type {HTMLFormElement} */
        this.newItemForm = this.newItemFormContainer.querySelector('.open-checklist-body-new-item-form');
        
        /** @type {HTMLInputElement} */
        this.newItemFormInput = this.newItemForm.querySelector('input[type="text"]');

        /** @type {HTMLButtonElement} */
        this.newItemFormSubmitButton = this.newItemForm.querySelector('.btn-create-item');
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
    
    get newItemFormInputValue()
    {
        return this.elements.newItemFormInput.value;
    }

    set newItemFormInputValue(value)
    {
        this.elements.newItemFormInput.value = value;
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


        this.elements.newItemForm.addEventListener(NativeEvents.SUBMIT, async (e) =>
        {
            e.preventDefault();
            await this.#createNewItem();
        });

        ChecklistItemDeleteButtonClickedEvent.addListener(async (e) => 
        {
            if (e.data.checklistId == this.checklistId)
            {
                await this.#deleteChecklistItem(e.data.itemId);
            }
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
            this.#addChecklistItemHtml(itemHtmlElement);
        }
    }


    /**
     * Add checklist item to the collection
     * @param {HTMLElement} itemHtmlElement the item's html
     */
    #addChecklistItemHtml = (itemHtmlElement) =>
    {
        const openChecklistItem = new OpenChecklistItem(itemHtmlElement, this.checklistId);
        this.checklistItems.push(openChecklistItem);
    }


    showChecklistItemsSpinner = () =>
    {
        this.elements.checklistItemsBody.classList.add(OpenChecklistElements.ChecklistItemsLoadingClass);
    }

    hideChecklistItemsSpinner = () =>
    {
        this.elements.checklistItemsBody.classList.remove(OpenChecklistElements.ChecklistItemsLoadingClass);
    }



    #createNewItem = async () =>
    {
        if (this.newItemFormInputValue.length < 1)
        {
            return;
        }

        const data = this.#getCreateChecklistItemForm();

        this.newItemFormInputValue = "";

        const itemHtml = await this.checklistItemServices.createNewItem(data);
        this.elements.itemsContainer.insertAdjacentHTML("beforeend", itemHtml);
        
        this.#initOpenChecklistItemsFromHtml();
    }

    #getCreateChecklistItemForm = () =>
    {
        // const position = this.checklistItems.length;
        const position = this.#getNextItemPosition();
        const createChecklistItemForm = new CreateChecklistItemForm(this.newItemFormInputValue, position);
        return createChecklistItemForm;
    }


    #getNextItemPosition = () =>
    {
        const sortedItems = this.checklistItems.toSorted((a, b) => {
            return a.elements.positionAttributeValue > b.elements.checklistItemIdAttributeValue;
        });

        if (sortedItems.length == 0)
        {
            return 1;
        }

        const lastElement = sortedItems.pop();
        return lastElement.elements.positionAttributeValue + 1;
    }


    #deleteChecklistItem = async (itemId) =>
    {
        this.checklistItemServices.deleteChecklistItem(itemId);

        const checklistItem = this.#getChecklistItem(itemId);
        checklistItem.remove();

        this.checklistItems = this.checklistItems.filter(c => c.itemId !== itemId);
    }




    /**
     * Get the specified checklist
     * @param {string} checklistId 
     */
    #getChecklistItem = (itemId) =>
    {
        const index = this.#getChecklistItemIndex(itemId);
        return this.checklistItems[index];
    }


    /**
     * Get the index of the specified checklist within the objects openChecklists collection
     * @param {string} itemId 
     */
    #getChecklistItemIndex = (itemId) =>
    {
        const index = this.checklistItems.findIndex(c => c.itemId === itemId);

        if (index == -1)
        {
            throw new Error(`There is no checklist item with this id: ${itemId}`);
        }

        return index;
    }
    
}


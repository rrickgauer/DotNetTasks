import { NativeEvents } from "../../domain/constants/native-events";
import { ChecklistItemDeleteButtonClickedEvent, ChecklistItemMoveItemDownButtonClickedEvent, ChecklistItemMoveItemUpButtonClickedEvent, DeleteChecklistEvent, OpenChecklistCloseButtonClickedEvent } from "../../domain/events/events";
import { CreateChecklistItemForm } from "../../domain/forms/checklist-item-forms";
import { ChecklistItemServices } from "../../services/checklist-item-services";
import { ChecklistLabelServices } from "../../services/checklist-label-services";
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

        /** @type {HTMLDivElement} */
        this.labelsContainer = this.container.querySelector('.open-checklist-body-labels');
    }


    #getContainer = () =>
    {
        const containerSelectorText = OpenChecklistElements.getSelector(this.checklistId);
        return document.querySelector(containerSelectorText);
    }
    
}


export class OpenChecklist
{
    //#region - Fields -

    /** @type {boolean} */
    #isMetaDataLoaded = false;

    /** @type {string} */
    checklistId;
    
    /** @type {ChecklistServices} */
    #checklistServices;
    
    /** @type {ChecklistItemServices} */
    #checklistItemServices;
    
    /** @type {string} */
    #html;

    /** @type {OpenChecklistElements} */
    #elements;

    /** @type {OpenChecklistItem[]} */
    #checklistItems;

    /** @type {ChecklistLabelServices} */
    #checklistLabelService;

    //#endregion


    //#region - Properties -

    get isMetaDataLoaded()
    {
        return this.#isMetaDataLoaded;
    }
    
    get newItemFormInputValue()
    {
        return this.#elements.newItemFormInput.value;
    }

    set newItemFormInputValue(value)
    {
        this.#elements.newItemFormInput.value = value;
    }

    //#endregion


    /**
     * Constructor
     * @param {string} checklistId the checklist id
     */
    constructor(checklistId)
    {
        this.checklistId           = checklistId;
        this.#checklistServices     = new ChecklistServices();
        this.#checklistItemServices = new ChecklistItemServices(this.checklistId);
        this.#checklistLabelService = new ChecklistLabelServices(this.checklistId);
        this.#html                  = null;
        this.#elements              = null;
        this.#checklistItems        = [];
    }


    /**
     * Close the checklist
     */
    close = () =>
    {
        this.#elements.container.remove();
    }


    /**
     * Fetch the metadata for the checklist
     */
    fetchMetaData = async () =>
    {
        this.#html = await this.#checklistServices.getChecklistHtml(this.checklistId);
        this.#isMetaDataLoaded = true;
    }

    /**
     * Append this checklist to the container
     * @param {HTMLElement} container the container to add this checklist to
     */
    appendChecklistToContainer = (container) =>
    {
        $(container).append(this.#html);
        this.#elements = new OpenChecklistElements(this.checklistId);
        this.#addListeners();
    }

    /**
     * Add the event listeners to the page
     */
    #addListeners = () =>
    {
        this.#elements.closeButton.addEventListener(NativeEvents.Click, (e) => 
        {
            OpenChecklistCloseButtonClickedEvent.invoke(this, this.checklistId);
        });

        this.#elements.deleteButton.addEventListener(NativeEvents.Click, (e) =>
        {
            DeleteChecklistEvent.invoke(this, this.checklistId);
        });


        this.#elements.newItemForm.addEventListener(NativeEvents.Submit, async (e) =>
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

        ChecklistItemMoveItemUpButtonClickedEvent.addListener(async (e) => {
            if (e.data.checklistId === this.checklistId)
            {
                await this.#moveItemUp(e.caller.itemId);
            }
        });


        ChecklistItemMoveItemDownButtonClickedEvent.addListener(async (e) => {
            if (e.data.checklistId === this.checklistId)
            {
                await this.#moveItemDown(e.caller.itemId);
            }
        });
    }


    //#region - Move Items -

    /**
     * Move the specified checklist item up
     * @param {string} checklistItemId the checklist item id
     */
    #moveItemUp = async (checklistItemId) =>
    {
        const neighbors = this.#getChecklistItemNeighbors(checklistItemId);
        
        if (!neighbors.isFirst)
        {
            await this.#moveItems(neighbors.current, neighbors.above);    
        }
    }

    /**
     * Move the specified checklist item down
     * @param {string} itemId the checklist item id
     */
    #moveItemDown = async (itemId) =>
    {
        const neighbors = this.#getChecklistItemNeighbors(itemId);
        
        if (!neighbors.isLast)
        {
            await this.#moveItems(neighbors.current, neighbors.below);
        }
    }

    /**
     * Swap the given OpenChecklistItems' positions
     * @param {OpenChecklistItem} item1 
     * @param {OpenChecklistItem} item2 
     */
    #moveItems = async (item1, item2) =>
    {
        this.showChecklistItemsSpinner();

        try
        {
            await this.#saveItemPositionsSwap(item1, item2);
            await this.#fetchItems();
            // await this.fetchChecklistData();
        }
        catch(error)
        {
            alert('there was an error moving the item');
            console.error(error);
        }
        finally
        {
            this.hideChecklistItemsSpinner();
        }
    }


    /**
     * Get the specified OpenChecklistItem's neighbors
     * @param {string} itemId the checklist item id
     */
    #getChecklistItemNeighbors = (itemId) =>
    {
        const currentIndex = this.#getChecklistItemIndex(itemId);
        
        const current = this.#checklistItems[currentIndex];
        const above = this.#isItemFirstInList(itemId) ? null : this.#checklistItems[currentIndex - 1];
        const below = this.#isItemLastInList(itemId) ? null : this.#checklistItems[currentIndex + 1];

        const neighbors = new ChecklistItemNeighbors(current, above, below);

        return neighbors;
    }

    /**
     * Is the specified item first in the collection?
     * @param {string} checklistItemId the checklist item id
     */
    #isItemFirstInList = (checklistItemId) =>
    {
        const itemIndex = this.#getChecklistItemIndex(checklistItemId);

        if (itemIndex === 0)
        {
            return true;
        }

        return false;
    }

    /**
     * Is the item last in the collection?
     * @param {string} checklistItemId the checklist item id
     */
    #isItemLastInList = (checklistItemId) =>
    {
        const itemIndex = this.#getChecklistItemIndex(checklistItemId);

        const elementsCount = this.#checklistItems.length;
        const lastItemIndex = elementsCount - 1;

        if (itemIndex === lastItemIndex)
        {
            return true;
        }

        return false;
    }

    /**
     * Swap the 2 given item's positions
     * @param {OpenChecklistItem} item1 
     * @param {OpenChecklistItem} item2 
     */
    #saveItemPositionsSwap = async (item1, item2) => 
    {
        const tempPosition = item1.position;
        item1.position = item2.position;
        item2.position = tempPosition;
        
        const updateForm1 = item1.getUpdateChecklistItemForm();
        const updateForm2 = item2.getUpdateChecklistItemForm();

        await this.#checklistItemServices.updateChecklistItem(item1.itemId, updateForm1);
        await this.#checklistItemServices.updateChecklistItem(item2.itemId, updateForm2);
    }

    //#endregion


    /**
     * Fetch the checklist's items
     */
    fetchChecklistData = async () =>
    {
        await this.#fetchItems();
        await this.#fetchChecklistLabels();
    }

    //#region Fetch items

    /**
     * Fetch the checklist items
     */
    #fetchItems = async () =>
    {
        const checklistItemsHtml = await this.#checklistItemServices.getChecklistItemsHtml();
        this.#elements.itemsContainer.innerHTML = checklistItemsHtml;
        this.#initOpenChecklistItemsFromHtml();
    }


    /**
     * Initialize the collection of checklist items from the inner html elements
     */
    #initOpenChecklistItemsFromHtml = () =>
    {
        this.#checklistItems = [];
        
        const selector = `.${OpenChecklistItemElements.ContainerClass}`;
        const itemsHtml = this.#elements.itemsContainer.querySelectorAll(selector);

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
        this.#checklistItems.push(openChecklistItem);
    }

    //#endregion


    //#region Fetch labels

    #fetchChecklistLabels = async () =>
    {
        const labelsHtml = await this.#checklistLabelService.getAssignedLabelsHtml();

        this.#elements.labelsContainer.innerHTML = labelsHtml;

        // $(this.#elements.labelsContainer).append(labelsHtml);
    }


    //#endregion





    /**
     * Show the checklist item spinner
     */
    showChecklistItemsSpinner = () =>
    {
        this.#elements.checklistItemsBody.classList.add(OpenChecklistElements.ChecklistItemsLoadingClass);
    }

    /**
     * Hide the checklist item spinner
     */
    hideChecklistItemsSpinner = () =>
    {
        this.#elements.checklistItemsBody.classList.remove(OpenChecklistElements.ChecklistItemsLoadingClass);
    }


    /**
     * Create a new checklist item
     */
    #createNewItem = async () =>
    {
        if (this.newItemFormInputValue.length < 1)
        {
            return;
        }

        const data = this.#getCreateChecklistItemForm();

        this.newItemFormInputValue = "";

        const itemHtml = await this.#checklistItemServices.createNewItem(data);
        this.#elements.itemsContainer.insertAdjacentHTML("beforeend", itemHtml);
        
        this.#initOpenChecklistItemsFromHtml();
    }

    /**
     * Create a new CreateChecklistItemForm object
     */
    #getCreateChecklistItemForm = () =>
    {
        const position = this.#getLargestItemPositionValue() + 1;

        const createChecklistItemForm = new CreateChecklistItemForm(this.newItemFormInputValue, position);

        return createChecklistItemForm;
    }


    /**
     * Get the largest checklist item's position value
     * @returns the greatest position value for the current checklist items
     */
    #getLargestItemPositionValue = () =>
    {
        // if no elements return 0
        if (this.#checklistItems.length === 0)
        {
            return 0;
        }
        // if only 1 element return its position
        else if (this.#checklistItems.length === 1)
        {
            return this.#checklistItems[0].position;
        }

        // sort the items by position
        const sortedItems = this.#checklistItems.toSorted((a, b) => {
            return a.position > b.position;
        });

        // return the last element's (the one with the greatest position value)
        const lastElement = sortedItems.pop();
        return lastElement.position;
    }

    /**
     * Delete the specified checklist item
     * @param {string} checklistItemId the checklist item id
     */
    #deleteChecklistItem = async (checklistItemId) =>
    {
        this.#checklistItemServices.deleteChecklistItem(checklistItemId);

        const checklistItem = this.#getChecklistItem(checklistItemId);
        checklistItem.remove();

        this.#checklistItems = this.#checklistItems.filter(c => c.itemId !== checklistItemId);
    }

    /**
     * Get the specified checklist
     * @param {string} checklistId 
     */
    #getChecklistItem = (checklistItemId) =>
    {
        const index = this.#getChecklistItemIndex(checklistItemId);
        return this.#checklistItems[index];
    }


    /**
     * Get the index of the specified checklist within the objects openChecklists collection
     * @param {string} itemId 
     */
    #getChecklistItemIndex = (itemId) =>
    {
        const index = this.#checklistItems.findIndex(c => c.itemId === itemId);

        if (index == -1)
        {
            throw new Error(`There is no checklist item with this id: ${itemId}`);
        }

        return index;
    }
    
}


export class ChecklistItemNeighbors
{
    /** @type {OpenChecklistItem} */
    current;
    
    /** @type {OpenChecklistItem} */
    above;
    
    /** @type {OpenChecklistItem} */
    below;


    /**
     * Checklist item neighbors
     * @param {OpenChecklistItem} current 
     * @param {OpenChecklistItem} above 
     * @param {OpenChecklistItem} below 
     */
    constructor(current, above=null, below=null)
    {
        this.current = current;
        this.above = above;
        this.below = below;
    }


    get isFirst()
    {
        return this.above === null;
    } 
    
    get isLast()
    {
        return this.below === null;
    }
}
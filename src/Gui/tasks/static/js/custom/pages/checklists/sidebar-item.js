import { NativeEvents } from "../../domain/constants/native-events";
import { ChecklistSidebarClickedEvent, ChecklistsSidebarItemClosedEvent, ChecklistsSidebarItemOpenedEvent } from "../../domain/events/events";
import { ChecklistsPageUrlWrapper } from "./url-wrapper";



export class ChecklistSidebarItem 
{
    static ActiveClass = 'active';
    static ChecklistIdAttribute = 'data-checklist-id';
    static ContainerClass = 'list-group-item';


    /**
     * Create instantiated ChecklistSidebarItem objects from every item in the specified container
     * @param {HTMLElement} container 
     */
    static getAllInContainer = (container) =>
    {
        const htmlElements = container.querySelectorAll(`.${ChecklistSidebarItem.ContainerClass}`);

        return Array.from(htmlElements, e => new ChecklistSidebarItem(e));
    }



    /**
     * Create an object from the specified checklist id
     * @param {string} checklistId the checklist id
     * @returns the SidebarItem
     */
    static fromId = (checklistId) =>
    {
        const selectorText = `.${ChecklistSidebarItem}[${ChecklistSidebarItem.ChecklistIdAttribute}="${checklistId}"]`;
        const itemHtmlElement = document.querySelector(selectorText);
        return new ChecklistSidebarItem(itemHtmlElement);
    }


    /**
     * Constructor
     * @param {HTMLElement} innerHtmlElement 
     */
    constructor(innerHtmlElement)
    {
        /** @type {HTMLButtonElement} */
        this.container = innerHtmlElement.closest(`.${ChecklistSidebarItem.ContainerClass}`);

        this.urlWrapper = ChecklistsPageUrlWrapper.fromCurrentUrl();

        this.#addListeners();
    }


    #addListeners = () =>
    {
        this.container.addEventListener(NativeEvents.CLICK, (e) => {
            this.toggle();
        });
    }



    /**
     * @returns {String}
     */
    get checklistId()
    {
        return this.container.getAttribute(ChecklistSidebarItem.ChecklistIdAttribute);
    }


    //#region Open/Close checklist

    /**
     * Toggle the item's active class
     */
    toggle = async () =>
    {

        if (this.isActive)
        {
            await this.closeChecklist();
        }
        else
        {
            await this.openChecklist();
        }
    }


    /**
     * Open the checklist
     */
    openChecklist = async () =>
    {
        this.urlWrapper.add(this.checklistId);
        this.isActive = true;
        ChecklistsSidebarItemOpenedEvent.invoke(this, this.checklistId);
    }

    /**
     * Close the checklist
     */
    closeChecklist = async () =>
    {
        this.urlWrapper.remove(this.checklistId);
        this.isActive = false;
        ChecklistsSidebarItemClosedEvent.invoke(this, this.checklistId);
    }

    //#endregion


    //#region Set the active class

    /**
     * @returns {Boolean}
     */
    get isActive()
    {
        return this.container.classList.contains(ChecklistSidebarItem.ActiveClass);
    }


    /**
     * Set the item's active class
     */
    set isActive(active)
    {
        if (active)
        {
            this.#addActiveClass();
        }
        else
        {
            this.#removeActiveClass();
        }
    }

    /**
     * Set the item to active.
     */
    #addActiveClass = () => this.container.classList.add(ChecklistSidebarItem.ActiveClass);

    /**
     * Remove the item's active class
     */
    #removeActiveClass = () => this.container.classList.remove(ChecklistSidebarItem.ActiveClass);

    //#endregion





}



import { ChecklistsPageUrlWrapper } from "./url-wrapper";




export class ChecklistSidebarItem 
{
    static ActiveClass = 'active';
    static ChecklistIdAttribute = 'data-checklist-id';
    static ContainerClass = 'list-group-item';

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
    }


    /**
     * @returns {Boolean}
     */
    get isActive()
    {
        return this.container.classList.contains(ChecklistSidebarItem.ActiveClass);
    }


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


    #addActiveClass = () =>
    {
        this.container.classList.add(ChecklistSidebarItem.ActiveClass);
    }

    #removeActiveClass = () =>
    {
        this.container.classList.remove(ChecklistSidebarItem.ActiveClass);
    }



    /**
     * @returns {String}
     */
    get checklistId()
    {
        return this.container.getAttribute(ChecklistSidebarItem.ChecklistIdAttribute);
    }


    
    toggle = async () =>
    {
        // console.log(this.isActive);

        if (this.isActive)
        {
            await this.closeChecklist();
        }
        else
        {
            await this.openChecklist();
        }
    }


    openChecklist = async () =>
    {
        this.urlWrapper.add(this.checklistId);
        this.isActive = true;
    }

    closeChecklist = async () =>
    {
        this.urlWrapper.remove(this.checklistId);
        this.isActive = false;
    }





}



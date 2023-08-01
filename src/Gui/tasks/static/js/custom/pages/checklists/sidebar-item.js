



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
    }


    /**
     * @returns {Boolean}
     */
    get isActive()
    {
        return this.container.classList.contains(ChecklistSidebarItem.ActiveClass);
    }

    /**
     * @returns {String}
     */
    get id()
    {
        return this.container.getAttribute(ChecklistSidebarItem.ChecklistIdAttribute);
    }


    
    toggle = async () =>
    {
        if (this.isActive)
        {
            await this.openChecklist();
        }
        else
        {
            await this.closeChecklist();
        }
    }


    openChecklist = async () =>
    {

    }

    closeChecklist = async () =>
    {
        
    }





}



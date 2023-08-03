import { NativeEvents } from "../../domain/constants/native-events";
import { BaseEventDetail } from "../../domain/events/detail";
import { ChecklistsOverlayClickedEvent, NewChecklistFormSubmittedEvent, NewChecklistFormToggleEvent } from "../../domain/events/events";
import { ChecklistServices } from "../../services/checklist-services";
import { NewChecklistFormController, NewChecklistFormElements } from "./new-checklist-form";
import { ChecklistsOverlay } from "./overlay";
import { ChecklistSidebarItem } from "./sidebar-item";
import { UrlWrapper } from "./url-wrapper";


export class SidebarElements
{
    static ContainerVisibility = 'active';

    constructor()
    {
        /** @type {HTMLDivElement} */
        this.container = document.querySelector('.checklists-sidebar');

        /** @type {HTMLButtonElement} */
        this.closeSidebarButton = this.container.querySelector('.btn-close-checklist');

        /** @type {HTMLButtonElement} */
        this.openNewChecklistFormButton = this.container.querySelector('.btn-new-checklist');

        /** @type {HTMLDivElement} */
        this.sidebarItemsContainer = this.container.querySelector('.checklist-sidebar-items');

        this.newChecklistForm = new NewChecklistFormElements(this.container);

        /** @type {HTMLDivElement} */
        this.checklistsItemsContainer = this.container.querySelector('.checklist-sidebar-items');
    }
}




export class SidebarController
{

    /**
     * Constructor
     */
    constructor()
    {
        this.sidebar = new SidebarElements();
        this.newChecklistForm = new NewChecklistFormController();
        this.services = new ChecklistServices();
        this.urlWrapper = UrlWrapper.fromCurrentUrl();
        this.overlay = new ChecklistsOverlay();

        /** @type {ChecklistSidebarItem[]} */
        this.sidebarItems = [];
    }

    /**
     * Initialize the object
     */
    init = async () =>
    {
        this.#addEventListeners();
        await this.fetchChecklists();
    }

    /**
     * Add the event listeners to the page
     */
    #addEventListeners = () =>
    {
        this.sidebar.closeSidebarButton.addEventListener(NativeEvents.CLICK, this.closeSidebar);
        
        this.sidebar.openNewChecklistFormButton.addEventListener(NativeEvents.CLICK, (e) => 
        {
            NewChecklistFormToggleEvent.invoke(this);
        });

        NewChecklistFormSubmittedEvent.addListener(this.#onNewChecklistFormSubmittedEvent);

        ChecklistsOverlayClickedEvent.addListener((e) => 
        {
            this.closeSidebar();
        });
    }

    //#region Toggle sidebar

    /** Close the sidebar */
    closeSidebar = () => 
    {
        this.sidebar.container.classList.remove(SidebarElements.ContainerVisibility);
        this.overlay.remove();
    }

    /** Open the sidebar */
    openSidebar = () => 
    {
        this.sidebar.container.classList.add(SidebarElements.ContainerVisibility);
        this.overlay.show();
    }
    
    //#endregion


    /**
     * Fetch all the checklists html
     */
    fetchChecklists = async () =>
    {
        const checklistsHtml = await this.services.getAllChecklistHtml();
        this.sidebar.sidebarItemsContainer.innerHTML = checklistsHtml;

        this.sidebarItems = ChecklistSidebarItem.getAllInContainer(this.sidebar.checklistsItemsContainer);
        this.#activateSidebarItems();
    }

    /**
     * If the url openChecklists search parm has any open checklist ids, activate the corresponding sidebar item.
     */
    #activateSidebarItems = () =>
    {
        const urlWrapper = UrlWrapper.fromCurrentUrl();
        const openChecklistIdsInUrl = urlWrapper.getOpenChecklistIds();

        if (openChecklistIdsInUrl.length == 0)
            return;

        for(const sidebarItem of this.sidebarItems)
        {
            if (openChecklistIdsInUrl.includes(sidebarItem.checklistId))
            {
                sidebarItem.isActive = true;
            }
        }
    }


    /**
     * Get the specified checklist
     * @param {string} checklistId 
     * @returns the checklist
     */
    getChecklist = (checklistId) =>
    {
        const index = this.getChecklistIndex(checklistId);
        return this.sidebarItems[index];
    }

    /**
     * Get the index of the specified checklist
     * @param {string} checklistId 
     * @returns the index
     */
    getChecklistIndex = (checklistId) =>
    {
        const index = this.sidebarItems.findIndex(s => s.checklistId === checklistId);

        if (index === -1)
        {
            throw new Error(`No sidebar checklist found with id = ${checklistId}`);
        }

        return index;
    }




    /**
     * Handle the new checklist form submission event
     * @param {BaseEventDetail} eventDetails 
     */
    #onNewChecklistFormSubmittedEvent = async (eventDetails) =>
    {
        await this.fetchChecklists();
        NewChecklistFormToggleEvent.invoke(this);
    }


}
import { NativeEvents } from "../../domain/constants/native-events";
import { BaseEventDetail } from "../../domain/events/detail";
import { ChecklistsOverlayClickedEvent, ChecklistsSidebarClosedEvent, ChecklistsSidebarItemClosedEvent, ChecklistsSidebarItemOpenedEvent, ChecklistsSidebarOpenedEvent, NewChecklistFormSubmittedEvent, NewChecklistFormToggleEvent } from "../../domain/events/events";
import { ChecklistServices } from "../../services/checklist-services";
import { ChecklistsElements } from "./checklist-elements";
import { ChecklistSidebarElements } from "./checklist-sidebar-elements";
import { NewChecklistFormController } from "./new-checklist-form";
import { ChecklistsOverlay } from "./overlay";
import { ChecklistSidebarItem } from "./sidebar-item";
import { ChecklistsPageUrlWrapper } from "./url-wrapper";


export class SidebarController
{

    /**
     * Constructor
     * @param {HTMLElement} container sidebar container html element
     */
    constructor(container)
    {
        this.sidebar = new ChecklistSidebarElements();
        this.elements = new ChecklistsElements(container);
        this.newChecklistForm = new NewChecklistFormController();
        this.services = new ChecklistServices();
        this.urlWrapper = ChecklistsPageUrlWrapper.fromCurrentUrl();
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
        await this.#fetchChecklists();
    }

    /**
     * Add the event listeners to the page
     */
    #addEventListeners = () =>
    {
        this.sidebar.closeSidebarButton.addEventListener(NativeEvents.CLICK, this.#closeSidebar);
        this.elements.openSidebarButton.addEventListener(NativeEvents.CLICK, this.#openSidebar);
        
        this.sidebar.openNewChecklistFormButton.addEventListener(NativeEvents.CLICK, (e) => {
            NewChecklistFormToggleEvent.invoke(this);
        });

        NewChecklistFormSubmittedEvent.addListener(this.#onNewChecklistFormSubmittedEvent);

        ChecklistsOverlayClickedEvent.addListener((e) => {
            this.#closeSidebar();
        });

        ChecklistsSidebarItemOpenedEvent.addListener(this.#onChecklistsSidebarItemOpenedEvent);
        ChecklistsSidebarItemClosedEvent.addListener(this.#onChecklistsSidebarItemClosedEvent);
    }


    #onChecklistsSidebarItemOpenedEvent = (eventDetail) =>
    {
        alert(`opened: ${eventDetail.data}`);
    }


    #onChecklistsSidebarItemClosedEvent = (eventDetail) =>
    {
        alert('closed');
    }





    //#region Toggle sidebar

    /** Close the sidebar */
    #closeSidebar = () => 
    {
        this.sidebar.container.classList.remove(ChecklistSidebarElements.ContainerVisibility);
        ChecklistsSidebarClosedEvent.invoke(this, null);
    }

    /** Open the sidebar */
    #openSidebar = () => 
    {
        this.sidebar.container.classList.add(ChecklistSidebarElements.ContainerVisibility);
        ChecklistsSidebarOpenedEvent.invoke(this, null);
    }
    
    //#endregion


    /**
     * Fetch all the checklists html
     */
    #fetchChecklists = async () =>
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
        const openChecklistIdsInUrl = this.urlWrapper.getOpenChecklistIds();

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
     * Handle the new checklist form submission event
     * @param {BaseEventDetail} eventDetails 
     */
    #onNewChecklistFormSubmittedEvent = async (eventDetails) =>
    {
        await this.#fetchChecklists();
        NewChecklistFormToggleEvent.invoke(this);
    }


}
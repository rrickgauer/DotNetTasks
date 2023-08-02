import { ChecklistServices } from "../../services/checklist-services";
import { ChecklistsElements } from "./checklist-elements";
import { ChecklistSidebarElements } from "./checklist-sidebar-elements";
import { NewChecklistFormController } from "./new-checklist-form";
import { ChecklistSidebarItem } from "./sidebar-item";
import { ChecklistsPageUrlWrapper } from "./url-wrapper";


export class SidebarController
{

    static OverlayClass = 'drawer-overlay';
    static eOverlay = `<div style="z-index: 109;" class="${SidebarController.OverlayClass}"></div>`;

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
        this.sidebar.closeSidebarButton.addEventListener('click', this.#closeSidebar);
        this.elements.openSidebarButton.addEventListener('click', this.#openSidebar);
        
        this.#listenForSidebarOverlayClick();

        this.sidebar.newChecklistButton.addEventListener('click', this.newChecklistForm.toggleNewChecklistForm);
        this.sidebar.newChecklistForm.buttonCancel.addEventListener('click', this.newChecklistForm.toggleNewChecklistForm);
        this.sidebar.newChecklistForm.form.addEventListener('submit', this.#createChecklist);
        
        this.#listenForChecklistClick();
    }

    /**
     * Listen for a click event of the main page overlay element
     */
    #listenForSidebarOverlayClick = () =>
    {
        document.querySelector('body').addEventListener('click', (e) => 
        {
            if (e.target.classList.contains(SidebarController.OverlayClass))
            {
                this.#closeSidebar();
            }
        });
    }

    /**
     * Listen for a sidebar checklist item click
     */
    #listenForChecklistClick = async () =>
    {
        this.sidebar.checklistsItemsContainer.addEventListener('click', async (e) =>
        {
            const parentContainer = e.target.closest('.list-group-item');

            if (parentContainer != null)
            {
                await this.#toggleChecklist(parentContainer);
            }
        });
    }


    /**
     * If the url openChecklists search parm has any open checklist ids, activate the corresponding sidebar item.
     */
    #activateItemsFromUrlSearchParm = () =>
    {
        const openChecklistIdsInUrl = this.urlWrapper.getOpenChecklistIds();

        if (openChecklistIdsInUrl.length == 0)
            return;

        const sidebarItems = ChecklistSidebarItem.getAllInContainer(this.sidebar.checklistsItemsContainer);

        for(const sidebarItem of sidebarItems)
        {
            if (openChecklistIdsInUrl.includes(sidebarItem.checklistId))
            {
                sidebarItem.isActive = true;
            }
        }
    }


    //#region Toggle sidebar

    /** Close the sidebar */
    #closeSidebar = () => 
    {
        this.sidebar.container.classList.remove(ChecklistSidebarElements.ContainerVisibility);
        document.querySelector(`.${SidebarController.OverlayClass}`).remove();
    }

    /** Open the sidebar */
    #openSidebar = () => 
    {
        this.sidebar.container.classList.add(ChecklistSidebarElements.ContainerVisibility);
        $('body').append(SidebarController.eOverlay);
    }
    
    //#endregion


    /**
     * Fetch all the checklists html
     */
    #fetchChecklists = async () =>
    {
        const checklistsHtml = await this.services.getAllChecklistHtml();
        this.sidebar.sidebarItemsContainer.innerHTML = checklistsHtml;

        this.#activateItemsFromUrlSearchParm();
    }

    /**
     * Handle the new checklist form submission event
     * @param {SubmitEvent} e the submit event
     */
    #createChecklist = async (e) =>
    {
        e.preventDefault();

        await this.newChecklistForm.submitForm();
        await this.#fetchChecklists();
        this.newChecklistForm.resetCloseForm();
    }


    /**
     * Toggle the sidebar checklist item.
     * @param {HTMLElement} clickedItem the clicked sidebar html element to toggle
     */
    #toggleChecklist = async (clickedItem) =>
    {
        const checklistItem = new ChecklistSidebarItem(clickedItem);
        await checklistItem.toggle();
    }
}
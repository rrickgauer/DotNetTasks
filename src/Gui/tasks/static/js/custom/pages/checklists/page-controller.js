import { NativeEvents } from "../../domain/constants/native-events";
import { ChecklistsSidebarItemClosedEvent, ChecklistsSidebarItemOpenedEvent, OpenChecklistCloseButtonClickedEvent } from "../../domain/events/events";
import { UrlWrapper } from "./url-wrapper";
import { SidebarController } from "./sidebar-controller";
import { OpenChecklistsController } from "./open-checklists-controller";


export class PageElements 
{
    /** @type {HTMLDivElement} */
    container = document.querySelector('.checklists-page-container');;

    /** @type {HTMLButtonElement} */
    openSidebarButton = this.container.querySelector('.btn-open-sidebar');
}



export class PageController
{

    constructor()
    {
        this.pageElements = new PageElements();
        this.sidebarController = new SidebarController();
        this.urlWrapper = new UrlWrapper(new URL(window.location.href));
        this.openChecklistsController = new OpenChecklistsController();
    }


    init = async () =>
    {
        this.#addEventListeners();
        await this.sidebarController.init();
        await this.openChecklistsController.init();
    }


    /**
     * Add the event listeners
     */
    #addEventListeners = () =>
    {
        this.pageElements.openSidebarButton.addEventListener(NativeEvents.CLICK, this.#openSidebar);

        ChecklistsSidebarItemOpenedEvent.addListener(async (e) => 
        {
            await this.#openChecklist(e.data);
        });

        ChecklistsSidebarItemClosedEvent.addListener((e) => 
        {
            this.#closeChecklist(e.data);
        });

        OpenChecklistCloseButtonClickedEvent.addListener((e) => 
        {
            this.#closeChecklist(e.data);
        });
    }


    /**
     * Open the sidebar
     */
    #openSidebar = () =>
    {
        this.sidebarController.openSidebar();
    }

    /**
     * Open the specified checklist
     * @param {string} checklistId the checklist to open
     */
    #openChecklist = async (checklistId) =>
    {
        this.urlWrapper.add(checklistId);
        await this.openChecklistsController.openChecklist(checklistId);
    }

    /**
     * Close the checklist
     * @param {string} checklistId the checklist that was closed
     */
    #closeChecklist = (checklistId) => 
    {
        this.urlWrapper.remove(checklistId);
        this.openChecklistsController.closeOpenChecklist(checklistId);
        this.sidebarController.getChecklist(checklistId).isActive = false;
    }




}
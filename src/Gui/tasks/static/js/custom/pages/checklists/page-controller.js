import { NativeEvents } from "../../domain/constants/native-events";
import { ChecklistsSidebarItemClosedEvent, ChecklistsSidebarItemOpenedEvent, DeleteChecklistEvent, OpenChecklistCloseButtonClickedEvent } from "../../domain/events/events";
import { UrlWrapper } from "./url-wrapper";
import { SidebarController } from "./sidebar-controller";
import { OpenChecklistsController } from "./open-checklists-controller";
import { ChecklistServices } from "../../services/checklist-services";
import { AlertPageTopSuccess } from "../../components/page-alerts/alert-page-top";


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
        this.checklistServices = new ChecklistServices();
    }


    init = async () =>
    {
        this.#addEventListeners();
        await this.sidebarController.init();

        const openChecklistIds = this.urlWrapper.getOpenChecklistIds();
        await this.openChecklistsController.init(openChecklistIds);
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

        DeleteChecklistEvent.addListener(async (e) => 
        {
            await this.#deleteChecklist(e.data);
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


    /**
     * Delete the specified checklist
     * @param {string} checklistId the checklist to delete
     */
    #deleteChecklist = async (checklistId) =>
    {
        if (confirm('Are you sure you want to delete this checklist? This cannot be undone.'))
        {
            this.#closeChecklist(checklistId);
            this.sidebarController.removeChecklist(checklistId);
            await this.#requestDelete(checklistId);
        }
    }

    /**
     * Use the service to delete the checklist.
     * Then, display a response alert.
     * @param {string} checklistId the checklist to delete
     */
    #requestDelete = async (checklistId) =>
    {
        const wasDeleted = await this.checklistServices.deleteChecklist(checklistId);

        if (wasDeleted)
        {
            const successfulAlert = new AlertPageTopSuccess('Checklist was deleted successfully');
            successfulAlert.show();
        }
        else
        {
            alert('Checklist could not be deleted');
        }
    }



}
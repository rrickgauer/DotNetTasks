import { ChecklistServices } from "../../services/checklist-services";
import { ChecklistSidebarElements } from "./checklist-sidebar-elements";
import { ChecklistsElements } from "./checklists-elements";
import { NewChecklistFormController } from "./new-checklist-form-controller";


export class ChecklistsController
{

    static OverlayClass = 'drawer-overlay';
    static eOverlay = `<div style="z-index: 109;" class="${ChecklistsController.OverlayClass}"></div>`;

    constructor(container)
    {
        this.sidebar = new ChecklistSidebarElements();
        this.elements = new ChecklistsElements(container);
        this.newChecklistForm = new NewChecklistFormController();
        this.services = new ChecklistServices();
    }

    init = async () =>
    {
        this.#addEventListeners();
        this.#fetchChecklists();
    }

    #addEventListeners = () =>
    {
        this.sidebar.closeSidebarButton.addEventListener('click', this.#closeSidebar);
        this.elements.openSidebarButton.addEventListener('click', this.#openSidebar);
        this.#listenForSidebarOverlayClick();
        this.sidebar.newChecklistButton.addEventListener('click', this.newChecklistForm.toggleNewChecklistForm);
        this.sidebar.newListFormButtonCancel.addEventListener('click', this.newChecklistForm.toggleNewChecklistForm);
        this.sidebar.newListFormInputTitle.addEventListener('keyup', this.newChecklistForm.updateSubmitButtonDisabled);
        this.sidebar.newListForm.addEventListener('submit', this.#createChecklist);
        this.#listenForChecklistClick();
    }

    #listenForSidebarOverlayClick = () =>
    {
        document.querySelector('body').addEventListener('click', (e) => 
        {
            if (e.target.classList.contains(ChecklistsController.OverlayClass))
            {
                this.#closeSidebar();
            }
        });
    }

    #listenForChecklistClick = async () =>
    {
        this.sidebar.checklistsItemsContainer.addEventListener('click', async (e) =>
        {

            const parentContainer = e.target.closest('.list-group-item');

            if (parentContainer != null)
            {
                const checklistId = parentContainer.getAttribute('data-checklist-id');
                await this.#toggleChecklist(checklistId);
            }
        });
    }



    //#region Toggle sidebar

    /** Close the sidebar */
    #closeSidebar = () => 
    {
        this.sidebar.container.classList.remove(ChecklistSidebarElements.ContainerVisibility);
        document.querySelector(`.${ChecklistsController.OverlayClass}`).remove();
    }

    /** Open the sidebar */
    #openSidebar = () => 
    {
        this.sidebar.container.classList.add(ChecklistSidebarElements.ContainerVisibility);
        $('body').append(ChecklistsController.eOverlay);
    }
    
    //#endregion


    #fetchChecklists = async () =>
    {
        const checklistsHtml = await this.services.getAllChecklistHtml();
        this.sidebar.sidebarItemsContainer.innerHTML = checklistsHtml;
    }

    #createChecklist = async (e) =>
    {
        e.preventDefault();
        await this.newChecklistForm.submitForm();
        await this.#fetchChecklists();
        this.newChecklistForm.resetCloseForm();
    }


    #toggleChecklist = async (checklistId) =>
    {
        alert(checklistId);
    }





}
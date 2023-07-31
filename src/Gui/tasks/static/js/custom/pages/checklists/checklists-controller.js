import { ApiChecklists } from "../../api/api-checklists";
import { ChecklistSidebarElements } from "./checklist-sidebar-elements";
import { ChecklistsElements } from "./checklists-elements";
import { NewChecklistFormController } from "./new-checklist-form-controller";


export class ChecklistsController
{

    static eOverlay = '<div style="z-index: 109;" class="drawer-overlay"></div>';

    constructor(container)
    {
        this.sidebar = new ChecklistSidebarElements();
        this.elements = new ChecklistsElements(container);
        this.newChecklistForm = new NewChecklistFormController();
        this.api = new ApiChecklists();
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
        this.sidebar.newListFormInputTitle.addEventListener('keyup', this.#updateNewChecklistFormSubmitButton);
    }

    #listenForSidebarOverlayClick = () =>
    {
        document.querySelector('body').addEventListener('click', (e) => 
        {
            if (e.target.classList.contains('drawer-overlay'))
            {
                this.#closeSidebar();
            }
        });
    }



    //#region Toggle sidebar

    /** Close the sidebar */
    #closeSidebar = () => 
    {
        this.sidebar.container.classList.remove(ChecklistSidebarElements.ContainerVisibility);
        document.querySelector('.drawer-overlay').remove();
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
        const response = await this.api.getAll();
        const checklistsHtml = await response.text();
        this.sidebar.sidebarItemsContainer.innerHTML = checklistsHtml;
    }

    #updateNewChecklistFormSubmitButton = () =>
    {
        if (this.newChecklistForm.inputValue().length == 0)
        {
            this.newChecklistForm.disableSubmitButton();
        }
        else
        {
            this.newChecklistForm.enableSubmitButton();
        }
    }





}
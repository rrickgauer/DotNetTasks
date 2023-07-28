



export class ChecklistSidebarElements
{

    static ContainerVisibility = 'active';

    constructor()
    {
        /** @type {HTMLDivElement} */
        this.container = document.querySelector('.checklists-sidebar');

        /** @type {HTMLButtonElement} */
        this.closeSidebarButton = this.container.querySelector('.btn-close-checklist');

        /** @type {HTMLButtonElement} */
        this.newChecklistButton = this.container.querySelector('.btn-new-checklist');

        /** @type {HTMLDivElement} */
        this.sidebarItemsContainer = this.container.querySelector('.checklist-sidebar-items');

        /** @type {HTMLDivElement} */
        this.newListFormContainer = this.container.querySelector('.new-checklist-form-container');

        /** @type {HTMLFormElement} */
        this.newListForm = this.newListFormContainer.querySelector('.new-checklist-form');

        /** @type {HTMLInputElement} */
        this.newListFormInputTitle = this.newListForm.querySelector('input[name="title"]');

        /** @type {HTMLButtonElement} */
        this.newListFormButtonSubmit = this.newListForm.querySelector('.save');

        /** @type {HTMLButtonElement} */
        this.newListFormButtonCancel = this.newListForm.querySelector('.cancel');
    }
}
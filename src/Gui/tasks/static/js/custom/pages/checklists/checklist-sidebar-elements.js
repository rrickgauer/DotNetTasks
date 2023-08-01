import { NewChecklistFormElements } from "./new-checklist-form";



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

        this.newChecklistForm = new NewChecklistFormElements(this.container);

        /** @type {HTMLDivElement} */
        this.checklistsItemsContainer = this.container.querySelector('.checklist-sidebar-items');
    }
}



export class AppSidebarLabelsFilterElements
{
    constructor()
    {
        /** @type {HTMLDivElement} */
        this.eModal = document.getElementById(AppSidebarLabelsFilterElements.CONTAINER);

        /** @type {HTMLFormElement} */
        this.eForm = this.eModal.getElementsByClassName(AppSidebarLabelsFilterElements.FORM)[0];
    }
}



AppSidebarLabelsFilterElements.CONTAINER = 'event-labels-filter-modal';
AppSidebarLabelsFilterElements.FORM = 'sidebar-container-labels-form';

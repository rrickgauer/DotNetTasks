


export class AppSidebarLabelsFilterElements
{
    constructor()
    {
        /** @type {HTMLDivElement} */
        this.eContainer = document.getElementById(AppSidebarLabelsFilterElements.CONTAINER);

        /** @type {HTMLFormElement} */
        this.eForm = this.eContainer.getElementsByClassName(AppSidebarLabelsFilterElements.FORM)[0];
    }
}



AppSidebarLabelsFilterElements.CONTAINER = 'sidebar-container';
AppSidebarLabelsFilterElements.FORM = 'sidebar-container-labels-form';

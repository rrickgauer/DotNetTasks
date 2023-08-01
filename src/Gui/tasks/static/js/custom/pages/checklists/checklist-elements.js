

export class ChecklistsElements {
    constructor(container) {
        /** @type {HTMLDivElement} */
        this.container = container;

        /** @type {HTMLButtonElement} */
        this.openSidebarButton = this.container.querySelector('.btn-open-sidebar');
    }
}

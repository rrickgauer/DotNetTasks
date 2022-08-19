import { SidebarContainerBtnActions } from "../../domain/enums/sidebar-container-btn-actions";

export class AppSidebarElements
{

    constructor()
    {   
        /** @type {HTMLDivElement} */
        this.eContainer = document.getElementById(AppSidebarElements.CONTAINER);

        /** @type {HTMLButtonElement} */
        this.eToggleCompletedEventsBtn = this._getBtnElementByAction(SidebarContainerBtnActions.TOGGLE_COMPLETED_EVENTS);
    }

    /**
     * Get the sidebar button based on the given custom data attribute value
     * @param {Number} btnAction the action button value
     * @returns {HTMLButtonElement}
     */
    _getBtnElementByAction = (btnAction) =>
    {
        const attrText = `[${AppSidebarElements.Attributes.BTN_ACTION}="${btnAction}"]`;

        const selector = `.${AppSidebarElements.BTN_CLASS}${attrText}`;

        return this.eContainer.querySelector(selector);
    }

}

AppSidebarElements.BTN_CLASS = 'sidebar-container-btn';
AppSidebarElements.CONTAINER = 'sidebar-container';

AppSidebarElements.Attributes =  {
    BTN_ACTION: 'data-js-action',
}
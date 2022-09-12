


export class LabelsListItemElements
{
    constructor(eContainer)
    {
        /** @type {HTMLDivElement} */
        this.eContainer = eContainer;

        /** @type {String} */
        this.id = this.eContainer.getAttribute(LabelsListItemElements.ID_ATTR);
    }
}


LabelsListItemElements.CONTAINER = 'labels-list-item';
LabelsListItemElements.ID_ATTR = 'data-js-id';

LabelsListItemElements.DROPDOWN_BTN = 'labels-list-item-dropdown-btn';
LabelsListItemElements.DROPDOWN_BTN_ATTR = 'data-js-dropdown-action';

LabelsListItemElements.DropdownBtnAttrs = {
    EDIT: 'edit',
    DELETE: 'delete',
}
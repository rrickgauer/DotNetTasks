


export class OpenChecklistItemElements
{
    static ContainerClass         = 'checklist-item';
    static DisplayContainerClass  = 'checklist-item-display';
    static CheckboxClass          = 'checklist-item-checkbox';
    static ContentClass           = 'checklist-item-content-text';
    static EditFormContainerClass = 'checklist-item-edit';
    static EditFormClass          = 'checklist-item-content-form';
    static EditFormTextInputClass = 'checklist-item-content-form-input';
    static EditFormButtonsClass   = 'checklist-item-content-form-btn';

    static ChecklistItemIdAttribute = 'data-checklist-item-id';

    constructor(container)
    {
        /** @type {HTMLDivElement} */
        this.container = container;

        /** @type {HTMLDivElement} */
        this.displayContainer = this.container.querySelector(`.${OpenChecklistItemElements.DisplayContainerClass}`);

        /** @type {HTMLInputElement} */
        this.checkbox = this.container.querySelector(`.${OpenChecklistItemElements.CheckboxClass}`);

        /** @type {HTMLSpanElement} */
        this.contentDisplay = this.container.querySelector(`.${OpenChecklistItemElements.ContentClass}`);

        /** @type {HTMLDivElement} */
        this.editFormContainer = this.container.querySelector(`.${OpenChecklistItemElements.EditFormContainerClass}`);

        /** @type {HTMLFormElement} */
        this.editForm = this.container.querySelector(`.${OpenChecklistItemElements.EditFormClass}`);

        /** @type {HTMLInputElement} */
        this.editFormInput = this.container.querySelector(`.${OpenChecklistItemElements.EditFormTextInputClass}`);

        /** @type {HTMLButtonElement} */
        this.editFormButtonSave = this.container.querySelector(`.${OpenChecklistItemElements.EditFormButtonsClass}.save`);

        /** @type {HTMLButtonElement} */
        this.editFormButtonSave = this.container.querySelector(`.${OpenChecklistItemElements.EditFormButtonsClass}.cancel`);
    }


    get checklistItemIdAttrValue()
    {
        return this.container.getAttribute(OpenChecklistItemElements.ChecklistItemIdAttribute);
    }

}



export class OpenChecklistItem
{
    constructor(htmlElement, checklistId)
    {
        this.elements = new OpenChecklistItemElements(htmlElement);
        this.checklistId = checklistId;
    }


    get itemId()
    {
        return this.elements.checklistItemIdAttrValue;
    }
}
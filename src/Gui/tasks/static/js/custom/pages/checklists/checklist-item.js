import { NativeEvents } from "../../domain/constants/native-events";
import { ChecklistItemServices } from "../../services/checklist-item-services";



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

    static IsCompleteClass = 'complete';

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


    get checklistItemIdAttributeValue()
    {
        return this.container.getAttribute(OpenChecklistItemElements.ChecklistItemIdAttribute);
    }


    get hasCompleteClass()
    {
        return this.container.classList.contains(OpenChecklistItemElements.IsCompleteClass);
    }

    set hasCompleteClass(complete)
    {
        if (complete)
        {
            this.container.classList.add(OpenChecklistItemElements.IsCompleteClass);
        }
        else
        {
            this.container.classList.remove(OpenChecklistItemElements.IsCompleteClass);
        }
    }


    toggleIsCompleteClass = () =>
    {
        this.container.classList.toggle(OpenChecklistItemElements.IsCompleteClass);
    }

}



export class OpenChecklistItem
{
    constructor(htmlElement, checklistId)
    {
        this.elements = new OpenChecklistItemElements(htmlElement);
        this.checklistId = checklistId;
        this.checklistItemService = new ChecklistItemServices(this.checklistId);
        this.#addEventListeners();
    }


    get itemId()
    {
        return this.elements.checklistItemIdAttributeValue;
    }


    #addEventListeners = () =>
    {
        this.elements.checkbox.addEventListener(NativeEvents.CHANGE, (e) => 
        {
            this.#toggleItemComplete();
        });
    }


    
    #toggleItemComplete = () =>
    {
        this.elements.toggleIsCompleteClass();

        if (this.elements.checkbox.checked)
        {
            this.checklistItemService.markItemComplete(this.itemId);
        }
        else
        {
            this.checklistItemService.markItemIncomplete(this.itemId);
        }
    }

}
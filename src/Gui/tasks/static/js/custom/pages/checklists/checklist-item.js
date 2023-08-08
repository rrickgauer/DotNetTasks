import { NativeEvents } from "../../domain/constants/native-events";
import { ChecklistItemServices } from "../../services/checklist-item-services";



export class DropdownActions
{
    static MoveUp = 'move-up';
    static MoveDown = 'move-down';
    static Duplicate = 'duplicate';
    static Delete = 'delete';
}



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
    static DropdownMenuClass      = 'checklist-item-dropdown-menu';
    static DropdownMenuItemClass  = 'dropdown-item';

    static ChecklistItemIdAttribute = 'data-checklist-item-id';
    static DropdownMenuItemActionAttribute = 'data-js-action';

    static ChecklistItemPositionAttribute = 'data-checklist-item-position';

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

        /** @type {HTMLDivElement} */
        this.dropdownMenu = this.container.querySelector(`.${OpenChecklistItemElements.DropdownMenuClass}`);

        /** @type {HTMLButtonElement} */
        this.dropdownItemMoveUp = this.#getDropdownMenuItemButton(DropdownActions.MoveUp);
        
        /** @type {HTMLButtonElement} */
        this.dropdownItemMoveDown = this.#getDropdownMenuItemButton(DropdownActions.MoveDown);
        
        /** @type {HTMLButtonElement} */
        this.dropdownItemDuplicate = this.#getDropdownMenuItemButton(DropdownActions.Duplicate);
        
        /** @type {HTMLButtonElement} */
        this.dropdownItemDelete = this.#getDropdownMenuItemButton(DropdownActions.Delete);
    }

    /**
     * 
     * @param {string} action action typr
     * @returns the button
     */
    #getDropdownMenuItemButton = (action) =>
    {
        const selector = `.${OpenChecklistItemElements.DropdownMenuItemClass}[${OpenChecklistItemElements.DropdownMenuItemActionAttribute}="${action}"]`
        
        /** @type {HTMLButtonElement} */
        const button = this.dropdownMenu.querySelector(selector);
        
        return button;
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


    get positionAttributeValue()
    {
        // return parseInt(this.container.getAttribute('data-checklist-item-position'));
        return parseInt(this.container.getAttribute(OpenChecklistItemElements.ChecklistItemPositionAttribute));
    }

    set positionAttributeValue(value)
    {
        this.container.setAttribute('data-checklist-item-position', value);
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


        this.elements.dropdownItemDelete.addEventListener(NativeEvents.CLICK, (e) =>
        {
            
        });

        this.elements.dropdownItemMoveUp.addEventListener(NativeEvents.CLICK, (e) =>
        {
            alert('moveup');
        });

        this.elements.dropdownItemMoveDown.addEventListener(NativeEvents.CLICK, (e) =>
        {
            alert('move down');
        });

        this.elements.dropdownItemDuplicate.addEventListener(NativeEvents.CLICK, (e) =>
        {
            alert('duplicate');
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
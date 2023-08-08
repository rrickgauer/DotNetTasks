import { NativeEvents } from "../../domain/constants/native-events";
import { ChecklistItemDeleteButtonClickedEvent } from "../../domain/events/events";
import { UpdateChecklistItemForm } from "../../domain/forms/checklist-item-forms";
import { ChecklistItemServices } from "../../services/checklist-item-services";



export class DropdownActions
{
    static MoveUp = 'move-up';
    static MoveDown = 'move-down';
    static Duplicate = 'duplicate';
    static Delete = 'delete';
    static MoveList = 'move-list'
    static Edit = 'edit';
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
    static ShowEditFormClass = 'edit';

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
        this.editFormButtonCancel = this.container.querySelector(`.${OpenChecklistItemElements.EditFormButtonsClass}.cancel`);

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
        
        /** @type {HTMLButtonElement} */
        this.dropdownItemMoveList = this.#getDropdownMenuItemButton(DropdownActions.MoveList);

        /** @type {HTMLButtonElement} */
        this.dropdownItemEdit = this.#getDropdownMenuItemButton(DropdownActions.Edit);
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
        return parseInt(this.container.getAttribute(OpenChecklistItemElements.ChecklistItemPositionAttribute));
    }

    set positionAttributeValue(value)
    {
        this.container.setAttribute('data-checklist-item-position', value);
    }

}



export class OpenChecklistItem
{
    //#region - Getters/Setters

    get itemId()
    {
        return this.elements.checklistItemIdAttributeValue;
    }

    get position()
    {
        return this.elements.positionAttributeValue;
    }

    set position(value)
    {
        this.elements.positionAttributeValue = value;
    }

    get isEditFormVisible()
    {
        return this.elements.container.classList.contains(OpenChecklistItemElements.ShowEditFormClass);
    }

    set isEditFormVisible(value)
    {
        if (value)
        {
            this.elements.container.classList.add(OpenChecklistItemElements.ShowEditFormClass);
        }
        else
        {
            this.elements.container.classList.remove(OpenChecklistItemElements.ShowEditFormClass);
        }
    }

    get editFormInputValue()
    {
        return this.elements.editFormInput.value;
    }

    set editFormInputValue(value)
    {
        this.elements.editFormInput.value = value;
    }

    get contentDisplay()
    {
        return this.elements.contentDisplay.innerText;
    }

    set contentDisplay(value)
    {
        this.elements.contentDisplay.innerText = value;
    }

    //#endregion


    constructor(htmlElement, checklistId)
    {
        this.elements = new OpenChecklistItemElements(htmlElement);
        this.checklistId = checklistId;
        this.checklistItemService = new ChecklistItemServices(this.checklistId);
        this.#addEventListeners();
    }


    #addEventListeners = () =>
    {
        this.elements.checkbox.addEventListener(NativeEvents.Change, (e) => 
        {
            this.#toggleItemComplete();
        });


        this.elements.dropdownItemDelete.addEventListener(NativeEvents.Click, (e) =>
        {
            ChecklistItemDeleteButtonClickedEvent.invoke(this, {
                checklistId: this.checklistId,
                itemId: this.itemId,
            });
        });

        this.elements.dropdownItemMoveUp.addEventListener(NativeEvents.Click, (e) =>
        {
            alert('moveup');
        });

        this.elements.dropdownItemMoveDown.addEventListener(NativeEvents.Click, (e) =>
        {
            alert('move down');
        });

        this.elements.dropdownItemDuplicate.addEventListener(NativeEvents.Click, (e) =>
        {
            alert('duplicate');
        });

        this.elements.contentDisplay.addEventListener(NativeEvents.Click, (e) => {
            this.#openEditForm();
        });

        this.elements.dropdownItemEdit.addEventListener(NativeEvents.Click, (e) => {
            this.#openEditForm();
        });

        this.elements.editForm.addEventListener(NativeEvents.Submit, async (e) => {
            e.preventDefault();
            this.#handleEditFormSubmission();
        });

        this.elements.editFormButtonCancel.addEventListener(NativeEvents.Click, (e) => {
            this.#cancelEdit();
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


    remove = () =>
    {
        this.elements.container.remove();
    }


    #openEditForm = () =>
    {
        this.editFormInputValue = this.contentDisplay;
        this.isEditFormVisible = true;
    }

    #handleEditFormSubmission = async () =>
    {
        if (this.editFormInputValue.length === 0)
        {
            return;
        }

        this.#saveItemEdit();
        this.contentDisplay = this.editFormInputValue;
        this.isEditFormVisible = false;
    }

    #saveItemEdit = async () =>
    {
        const updateForm = new UpdateChecklistItemForm(this.editFormInputValue, this.position, this.elements.hasCompleteClass);

        try 
        {
            const response = await this.checklistItemService.updateChecklistItem(this.itemId, updateForm);
            console.log(response);
        }
        catch(error)
        {
            alert('Error saving changes to checklist item.');
            console.error(error);
        }        
    }



    #cancelEdit = () =>
    {
        this.isEditFormVisible = false;
    }

}
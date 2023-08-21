import { NativeEvents } from "../../../domain/constants/native-events";
import { ChecklistSettingsChecklistClonedEvent } from "../../../domain/events/events";
import { CloneChecklistForm } from "../../../domain/forms/clone-checklist-form";
import { SpinnerButton } from "../../../helpers/spinner-button";
import { Utililties } from "../../../helpers/utilities";
import { ChecklistServices } from "../../../services/checklist-services";



export class CloneChecklistElements
{
    constructor()
    {
        /** @type {HTMLFormElement} */
        this.form = document.querySelector('.checklist-settings-clone-form');
        
        /** @type {HTMLFieldSetElement} */
        this.fieldSet = this.form.querySelector('fieldset');
        
        /** @type {HTMLInputElement} */
        this.titleInput = this.form.querySelector('#checklist-settings-clone-form-input-title');
        
        /** @type {HTMLButtonElement} */
        this.submitButton = this.form.querySelector('.submit-clone-form');
    }
}




export class CloneChecklistController
{
    constructor(checklistId)
    {
        this.checklistId = checklistId;
        this.elements = new CloneChecklistElements();
        this.submitButtonSpinner = new SpinnerButton(this.elements.submitButton);
        this.checklistServices = new ChecklistServices();
    }


    get titleInputValue()
    {
        return this.elements.titleInput.value;
    }

    set titleInputValue(value)
    {
        this.elements.titleInput.value = value;
    }


    init = () =>
    {
        this.#addEventListeners();
    }

    #addEventListeners = () =>
    {
        this.elements.form.addEventListener(NativeEvents.Submit, async (e) =>
        {
            e.preventDefault();
            await this.#submitForm();
        }); 

        this.elements.titleInput.addEventListener(NativeEvents.KeyUp, this.#handleTitleInputChange);
    }

    #submitForm = async () =>
    {
        this.#disableForm();

        const cloneChecklistForm = new CloneChecklistForm(this.titleInputValue);
    
        try
        {
            const result = await this.checklistServices.cloneChecklist(this.checklistId, cloneChecklistForm);
            ChecklistSettingsChecklistClonedEvent.invoke(this, result);
        }
        catch(error)
        {
            alert('Failed to clone checklist');
        }
        finally
        {
            this.#enableForm();
            this.titleInputValue = "";
            this.#handleTitleInputChange();
        }

    }

    #disableForm = () =>
    {
        Utililties.disableElement(this.elements.fieldSet);
        this.submitButtonSpinner.showSpinner();
    }

    #enableForm = () =>
    {
        Utililties.enableElement(this.elements.fieldSet);
        this.submitButtonSpinner.reset();
    }


    #handleTitleInputChange = () =>
    {
        if (this.titleInputValue.length > 0)
        {
            Utililties.enableElement(this.elements.submitButton);
        }
        else
        {
            Utililties.disableElement(this.elements.submitButton);
        }
    }

}
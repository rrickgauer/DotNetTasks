import { NativeEvents } from "../../../domain/constants/native-events";
import { ChecklistSettingsGeneralFormSubmittedEvent } from "../../../domain/events/events";
import { UpdateChecklistForm } from "../../../domain/forms/update-checklist-form";
import { SpinnerButton } from "../../../helpers/spinner-button";
import { Utililties } from "../../../helpers/utilities";
import { ChecklistServices } from "../../../services/checklist-services";


export class GeneralSettingsFormElements
{
    constructor()
    {
        /** @type {HTMLFormElement} */
        this.form = document.querySelector('.checklists-settings-general-form');

        /** @type {HTMLInputElement} */
        this.inputTitle = document.querySelector('#form-input-title');

        /** @type {RadioNodeList} */
        this.listTypeRadio = this.form.elements.namedItem('form-input-radio');

        /** @type {HTMLButtonElement} */
        this.submitButton = this.form.querySelector('.submit-form');

        /** @type {HTMLFieldSetElement} */
        this.fieldSet = this.form.querySelector('fieldset');
    }
}


export class GeneralSettingsFormController
{

    /**
     * Constructor
     * @param {string} checklistId the current checklist id
     */
    constructor(checklistId)
    {
        /** @type {string} */
        this.checklistId = checklistId;

        this.elements = new GeneralSettingsFormElements();
        this.submitButtonSpinner = new SpinnerButton(this.elements.submitButton);
        this.checklistServices = new ChecklistServices();
    }

    get selectedTypeValue()
    {
        return this.elements.listTypeRadio.value;
    }

    get titleValue()
    {
        return this.elements.inputTitle.value;
    }

    #getUpdateChecklistForm = () =>
    {
        const updateChecklistForm = new UpdateChecklistForm(this.titleValue, this.selectedTypeValue);

        return updateChecklistForm;
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
            await this.#handleFormSubmission();
        });

        this.elements.inputTitle.addEventListener(NativeEvents.KeyUp, (e) =>
        {
            this.#toggleSubmitButton();
        });
    }


    #handleFormSubmission = async () =>
    {
        this.#disableForm();

        const formValues = this.#getUpdateChecklistForm();

        let successful = true;
        
        try
        {
            await this.checklistServices.saveChecklist(this.checklistId, formValues);
        }
        catch(error)
        {
            successful = false;
        }

        ChecklistSettingsGeneralFormSubmittedEvent.invoke(this, successful);
        this.#enableForm();


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



    #toggleSubmitButton = () =>
    {
        if (this.titleValue.length > 0)
        {
            Utililties.enableElement(this.elements.submitButton);
        }
        else
        {
            Utililties.disableElement(this.elements.submitButton);
        }
    }

}
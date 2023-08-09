import { BootstrapClasses } from "../../domain/constants/bootstrap-classes";
import { NativeEvents } from "../../domain/constants/native-events";
import { NewChecklistFormSubmittedEvent, NewChecklistFormToggleEvent } from "../../domain/events/events";
import { SpinnerButton } from "../../helpers/spinner-button";
import { Utililties } from "../../helpers/utilities";
import { ChecklistServices } from "../../services/checklist-services";


export class NewChecklistFormElements
{
    constructor()
    {
        /** @type {HTMLDivElement} */
        this.container = document.querySelector('.new-checklist-form-container');

        /** @type {HTMLFormElement} */
        this.form = this.container.querySelector('.new-checklist-form');

        /** @type {HTMLInputElement} */
        this.inputTitle = this.form.querySelector('input[name="title"]');

        /** @type {HTMLButtonElement} */
        this.buttonSubmit = this.form.querySelector('.save');
    }
}


export class NewChecklistFormController
{
    constructor()
    {
        this.elements = new NewChecklistFormElements();
        this.services = new ChecklistServices();
        this.submitButtonSpinner = new SpinnerButton(this.elements.buttonSubmit);

        this.#addEventListeners();
    }

    
    openNewChecklistForm   = () => this.elements.container.classList.remove(BootstrapClasses.DISPLAY_NONE);
    closeNewChecklistForm  = () => this.elements.container.classList.add(BootstrapClasses.DISPLAY_NONE);
    toggleNewChecklistForm = () => this.elements.container.classList.toggle(BootstrapClasses.DISPLAY_NONE);
    inputValue             = () => this.elements.inputTitle.value;
    enableSubmitButton     = () => Utililties.enableElement(this.elements.buttonSubmit);
    disableSubmitButton    = () => Utililties.disableElement(this.elements.buttonSubmit);


    #addEventListeners = () =>
    {
        this.elements.inputTitle.addEventListener(NativeEvents.KeyUp, this.#updateSubmitButtonDisabled);

        NewChecklistFormToggleEvent.addListener(this.toggleNewChecklistForm);

        this.#eventListenerFormSubmission();
    }


    #eventListenerFormSubmission = () =>
    {
        this.elements.form.addEventListener(NativeEvents.Submit, (e) => 
        {
            e.preventDefault();
            this.submitForm();
        });
    }



    /**
     * Enable or disable the submit button based on the input value length
     */
    #updateSubmitButtonDisabled = () =>
    {
        if (this.inputValue().length == 0)
        {
            this.disableSubmitButton();
        }
        else
        {
            this.enableSubmitButton();
        }
    }


    /**
     * Submit the new checklist form
     */
    submitForm = async () =>
    {
        this.submitButtonSpinner.showSpinner();
        const title = this.inputValue();

        try 
        {
            await this.services.createNewChecklist(title);
        }
        catch(error)
        {
            alert('Could not create a new checklist. Check console');
            console.error(error);
        }
        finally
        {
            this.submitButtonSpinner.reset();
            NewChecklistFormSubmittedEvent.invoke(this);
        }

    }

}
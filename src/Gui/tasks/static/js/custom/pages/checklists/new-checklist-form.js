import { SpinnerButton } from "../../helpers/spinner-button";
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

        /** @type {HTMLButtonElement} */
        this.buttonCancel = this.form.querySelector('.cancel');
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

    openNewChecklistForm   = () => this.elements.container.classList.remove('d-none');
    closeNewChecklistForm  = () => this.elements.container.classList.add('d-none');
    toggleNewChecklistForm = () => this.elements.container.classList.toggle('d-none');
    inputValue             = () => this.elements.inputTitle.value;
    enableSubmitButton     = () => this.elements.buttonSubmit.removeAttribute('disabled');
    disableSubmitButton    = () => this.elements.buttonSubmit.setAttribute('disabled', true);


    #addEventListeners = () =>
    {
        this.elements.inputTitle.addEventListener('keyup', this.updateSubmitButtonDisabled);
    }



    /**
     * Enable or disable the submit button based on the input value length
     */
    updateSubmitButtonDisabled = () =>
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
        }
    }


    /**
     * Reset and close the form
     */
    resetCloseForm = () =>
    {
        this.elements.form.reset();
        this.closeNewChecklistForm();
    }

}
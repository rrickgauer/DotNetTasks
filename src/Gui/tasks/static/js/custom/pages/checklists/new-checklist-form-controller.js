import { ChecklistSidebarElements } from "./checklist-sidebar-elements";



export class NewChecklistFormController
{
    constructor()
    {
        this.elements = new ChecklistSidebarElements();
    }

    openNewChecklistForm = () => this.elements.newListFormContainer.classList.remove('d-none');
    closeNewChecklistForm = () => this.elements.newListFormContainer.classList.add('d-none');
    toggleNewChecklistForm = () => this.elements.newListFormContainer.classList.toggle('d-none');

    inputValue = () => this.elements.newListFormInputTitle.value;
    
    enableSubmitButton = () => this.elements.newListFormButtonSubmit.removeAttribute('disabled');
    disableSubmitButton = () => this.elements.newListFormButtonSubmit.setAttribute('disabled', true);


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

}
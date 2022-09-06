import { LabelPageFormElements } from "./label-form-elements";


export class NewLabelPageFormController
{
    constructor()
    {
        /** @type {String} */
        this.modalId = LabelPageFormElements.ModalsIds.NEW;

        /** @type {LabelPageFormElements} */
        this.elements = new LabelPageFormElements(this.modalId);
    }

    /**
     * Adds the event listeners
     */
    addListeners = () =>
    {
        this._listenForModalClose();
    }


    /**
     * Listens for the model to be closed.
     * Resets the form
     */
    _listenForModalClose = () =>
    {
        $(this.elements.eModal).on('hidden.bs.modal', (e) => 
        {
            this.elements.eForm.reset();
        });
    }
}
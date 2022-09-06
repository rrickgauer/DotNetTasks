import { Label } from "../../../domain/models/label";
import { LabelPageFormElements } from "./label-form-elements";


export class EditLabelPageFormController
{
    constructor()
    {
        this.modalId = LabelPageFormElements.ModalsIds.EDIT;
        this.elements = new LabelPageFormElements(this.modalId);
    }

    showModal = () => $(this.elements.eModal).modal('show');

    /**
     * Set the form values  
     * @param {Label} label the label
     */
    setFormValues = (label) =>
    {
        this.elements.eInputColor.value = label.color;
        this.elements.eInputName.value = label.name;
        this.elements.setLabelIdAttr(label.id);
    }


    addListeners = () => 
    {
        this._listenForFormSubmission();
    }


    _listenForFormSubmission = () =>
    {
        this.elements.eForm.addEventListener('submit', (ev) => {
            ev.preventDefault();
            this._handleFormSubmission();
        });
    }

    _handleFormSubmission = () =>
    {
        alert('edit label');
    }
}
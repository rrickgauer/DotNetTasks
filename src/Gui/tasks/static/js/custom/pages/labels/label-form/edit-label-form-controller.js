import { LabelPageFormElements } from "./label-form-elements";


export class EditLabelPageFormController
{
    constructor()
    {
        this.modalId = LabelPageFormElements.ModalsIds.EDIT;
        this.elements = new LabelPageFormElements(this.modalId);
    }

    showModal = () => $(this.elements.eModal).modal('show');
}
import { LabelPageFormElements } from "./label-form-elements";


export class NewLabelPageFormController
{
    constructor()
    {
        this.modalId = LabelPageFormElements.ModalsIds.NEW;
        this.elements = new LabelPageFormElements(this.modalId);
    }
}
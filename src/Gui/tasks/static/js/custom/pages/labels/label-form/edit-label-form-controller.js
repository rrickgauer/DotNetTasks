import { ApiLabels } from "../../../api/api-labels";
import { ModifyLabelForm } from "../../../domain/forms/modify-label-form";
import { Label } from "../../../domain/models/label";
import { LabelPageFormElements } from "./label-form-elements";


export class EditLabelPageFormController
{
    constructor()
    {
        this.modalId = LabelPageFormElements.ModalsIds.EDIT;
        this.elements = new LabelPageFormElements(this.modalId);
        this.api = new ApiLabels();
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

    //#region Form submission

    /**
     * Listen for the form submission event
     */
    _listenForFormSubmission = () =>
    {
        this.elements.eForm.addEventListener('submit', (ev) => 
        {
            ev.preventDefault();
            this._handleFormSubmission();
        });
    }

    /**
     * Steps to take for submitting the form
     */
    _handleFormSubmission = async () =>
    {
        const formValues = this._getFormValues();
        const labelId = this.elements.getLabelIdAttr();

        const response = await this.api.put(labelId, formValues);

        console.log(await response.text());
    }


    /**
     * Get the current values of the form.
     * @returns {ModifyLabelForm}
     */
    _getFormValues = () =>
    {
        const result = new ModifyLabelForm();
        // result.id = this.elements.getLabelIdAttr();
        result.color = this.elements.eInputColor.value;
        result.name = this.elements.eInputName.value;

        return result;
    }

    //#endregion
}
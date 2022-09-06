import { ApiLabels } from "../../../api/api-labels";
import { AlertPageTopDanger, AlertPageTopSuccess } from "../../../components/page-alerts/alert-page-top";
import { ModifyLabelForm } from "../../../domain/forms/modify-label-form";
import { Label } from "../../../domain/models/label";
import { SpinnerButton } from "../../../helpers/spinner-button";
import { LabelsPageController } from "../page-actions/labels-page-controller";
import { LabelPageFormElements } from "./label-form-elements";

export class EditLabelPageFormController
{
    constructor()
    {
        this.modalId = LabelPageFormElements.ModalsIds.EDIT;
        this.elements = new LabelPageFormElements(this.modalId);
        this.api = new ApiLabels();
        this.spinnerBtn = new SpinnerButton(this.elements.eSubmitBtn);
        this.pageController = new LabelsPageController();

        this.successfulAlert = new AlertPageTopSuccess('Label was updated!');
        this.badAlert = new AlertPageTopDanger('Label was not updated. Check console.');
    }

    showModal = () => $(this.elements.eModal).modal('show');
    hideModal = () => $(this.elements.eModal).modal('hide');

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
     * Submit the form to the api
     */
    _handleFormSubmission = async () =>
    {
        // disable the form
        this.spinnerBtn.showSpinner();

        // gather the data from the form 
        const formValues = this._getFormValues();
        const labelId = this.elements.getLabelIdAttr();

        // send the request to the api
        const response = await this.api.put(labelId, formValues);

        // handle the api response
        if (response.ok)    
            await this._handleSuccessfulfulUpdateRequest();
        else
            await this._handleUnsuccessfulfulUpdateRequest(response);

        // reset everything
        this.spinnerBtn.reset();
        this.hideModal();
    }

    /**
     * Steps to take for a successul update api request
     */
    _handleSuccessfulfulUpdateRequest = async () =>
    {
        await this.pageController.renderLabelsHtml();
        this.successfulAlert.show();
    }

    /**
     * Steps to take for an unsuccessful api request for updating the label
     * @param {Promise<Response>} apiResponse 
     */
    _handleUnsuccessfulfulUpdateRequest = async (apiResponse) =>
    {
        const responseText = await apiResponse.text();
        console.error(responseText);

        this.badAlert.show();
    }


    /**
     * Get the current values of the form.
     * @returns {ModifyLabelForm}
     */
    _getFormValues = () =>
    {
        const result = new ModifyLabelForm();
        result.color = this.elements.eInputColor.value;
        result.name = this.elements.eInputName.value;

        return result;
    }

    //#endregion
}
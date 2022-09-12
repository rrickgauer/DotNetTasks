import { ApiLabels } from "../../../api/api-labels";
import { SpinnerButton } from "../../../helpers/spinner-button";
import { Utililties } from "../../../helpers/utilities";
import { LabelsPageController } from "../page-actions/labels-page-controller";
import { LabelPageFormElements } from "./label-form-elements";


export class NewLabelPageFormController
{
    constructor()
    {
        /** @type {String} */
        this.modalId = LabelPageFormElements.ModalsIds.NEW;

        /** @type {LabelPageFormElements} */
        this.elements = new LabelPageFormElements(this.modalId);

        /** @type {SpinnerButton} */
        this.spinnerButton = new SpinnerButton(this.elements.eSubmitBtn);

        /** @type {ApiLabels} */
        this.api = new ApiLabels();

        /** @type {LabelsPageController} */
        this.pageController = new LabelsPageController();
    }

    /**
     * Adds the event listeners
     */
    addListeners = () =>
    {
        this._listenForModalClose();
        this._listenForModalOpen();
        this._listenForFormSubmission();
    }


    //#region Modal closing/opening

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

    /**
     * Listen for the modal to be opened.
     * Set a new label id js attribute value
     */
    _listenForModalOpen = () =>
    {
        $(this.elements.eModal).on('shown.bs.modal', (e) => 
        {
            const newLabelId = Utililties.getNewUUID();
            this.elements.setLabelIdAttr(newLabelId);
        });
    }

    //#endregion


    /**
     * Event listeners for the form submission
     */
    _listenForFormSubmission = () =>
    {
        this.elements.eForm.addEventListener('submit', (e) =>
        {
            e.preventDefault();
            this._submitForm();
        });
    }


    _submitForm = async () =>
    {
        this.spinnerButton.showSpinner();

        const formValues = this.elements.getFormValues();
        const labelId = this.elements.getLabelIdAttr();
        
        const response = await this.api.put(labelId, formValues);

        if (!response.ok)
        {
            const responseData = await response.text();
            console.error(responseData);

            alert('There was an error. Check console');
        }

        await this.pageController.renderLabelsHtml();
        
        this.spinnerButton.reset();
        $(this.elements.eModal).modal('hide');
        
    }
}
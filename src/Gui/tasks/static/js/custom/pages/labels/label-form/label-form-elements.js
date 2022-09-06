


export class LabelPageFormElements
{

    constructor(modalId)
    {
        /** @type {String} */
        this.modalId = modalId;

        /** @type {HTMLDivElement} */
        this.eModal = document.getElementById(this.modalId);

        /** @type {HTMLFormElement} */
        this.eForm = this.eModal.querySelector(`.${LabelPageFormElements.FORM}`);

        /** @type {HTMLInputElement} */
        this.eInputName = this.eForm.querySelector(`input[name="${LabelPageFormElements.Inputs.NAME}"]`);

        /** @type {HTMLInputElement} */
        this.eInputColor = this.eForm.querySelector(`input[name="${LabelPageFormElements.Inputs.COLOR}"]`);

        /** @type {HTMLButtonElement} */
        this.eSubmitBtn = this.eModal.querySelector(`.${LabelPageFormElements.SUBMIT_BTN}`);
    }

    /**
     * Set the 'data-js-label-id' attribute value
     * @param {String} labelId the label id
     */
    setLabelIdAttr = (labelId) => this.eModal.setAttribute(LabelPageFormElements.LABEL_ID_ATTR, labelId);

    showLoading = () => this.eModal.classList.add(LabelPageFormElements.LOADING_CLASS);
    hideLoading = () => this.eModal.classList.remove(LabelPageFormElements.LOADING_CLASS);

}



LabelPageFormElements.MODAL = 'labels-form-modal';
LabelPageFormElements.LABEL_ID_ATTR = 'data-js-label-id';
LabelPageFormElements.FORM = 'labels-form';
LabelPageFormElements.LOADING_CLASS = 'loading';
LabelPageFormElements.SPINNER = 'labels-form-spinner';
LabelPageFormElements.SUBMIT_BTN = 'labels-form-submit-btn';

LabelPageFormElements.Inputs = {
    NAME: 'label-name',
    COLOR: 'label-color',
}

LabelPageFormElements.ModalsIds = {
    NEW: 'labels-form-modal-new',
    EDIT: 'labels-form-modal-edit',
}



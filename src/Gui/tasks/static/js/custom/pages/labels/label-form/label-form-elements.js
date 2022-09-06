


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


    //#region Get/Set Label ID attribute
    /**
     * Set the 'data-js-label-id' attribute value
     * @param {String} labelId the label id
     */
    setLabelIdAttr = (labelId) => this.eModal.setAttribute(LabelPageFormElements.LABEL_ID_ATTR, labelId);

    /**
     * Get the current data attribute id value for the label
     * @returns {String}
     */
    getLabelIdAttr = () => this.eModal.getAttribute(LabelPageFormElements.LABEL_ID_ATTR);

    //#endregion

    //#region Toggle loading
    showLoading = () => this.eModal.classList.add(LabelPageFormElements.LOADING_CLASS);
    hideLoading = () => this.eModal.classList.remove(LabelPageFormElements.LOADING_CLASS);
    //#endregion

    
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



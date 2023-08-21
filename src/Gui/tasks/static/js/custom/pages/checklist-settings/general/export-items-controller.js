import { BootstrapClasses } from "../../../domain/constants/bootstrap-classes";
import { NativeEvents } from "../../../domain/constants/native-events";
import { BootstrapUtilities } from "../../../helpers/bootstrap-utilities";
import { Utililties } from "../../../helpers/utilities";


export class ExportChecklistItem
{
    constructor(htmlElement)
    {
        /** @type {HTMLDivElement} */
        this.htmlElement = htmlElement;

        /** @type {HTMLInputElement} */
        this.input = this.htmlElement.querySelector('input');

        /** @type {HTMLLabelElement} */
        this.label = this.htmlElement.querySelector('label');
    }

    get id()
    {
        return this.input.value;
    }

    get content()
    {
        return this.label.innerText;
    }

    get isComplete()
    {
        return this.input.checked;
    }


    getOutputText = () =>
    {
        const box = this.isComplete ? '[x]' : '[ ]';
        const result = `- ${box} ${this.content}`;
        return result;
    }
}


export class ExportChecklistItemsElements
{
    static ModalClass = 'export-checklist-items-modal';
    static FormClass = 'export-items-form';
    static CheckboxClass = 'export-items-form-checkbox';
    static CopyToClipboardButtonClass = 'btn-copy-to-clipboard';
    static StatusMessageClass = 'copied-status-message';

    constructor()
    {
        /** @type {HTMLDivElement} */
        this.modal = document.querySelector(`.${ExportChecklistItemsElements.ModalClass}`);

        /** @type {HTMLFormElement} */
        this.form = this.modal.querySelector(`.${ExportChecklistItemsElements.FormClass}`);

        /** @type {HTMLButtonElement} */
        this.copyToClipboardButton = this.modal.querySelector(`.${ExportChecklistItemsElements.CopyToClipboardButtonClass}`);

        /** @type {HTMLParagraphElement} */
        this.statusMessage = this.modal.querySelector(`.${ExportChecklistItemsElements.StatusMessageClass}`);

        /** @type {ExportChecklistItem[]} */
        this.checkboxes = this.#getCheckboxesCollection();
    }

    #getCheckboxesCollection = () =>
    {
        const checkboxes = [];
        
        const htmlElements = this.form.querySelectorAll(`.${ExportChecklistItemsElements.CheckboxClass}`);

        for (const e of htmlElements)
        {
            const checkbox = new ExportChecklistItem(e);
            checkboxes.push(checkbox);
        }

        return checkboxes;
    }
}


export class ExportChecklistItemsController
{
    constructor()
    {
        this.elements = new ExportChecklistItemsElements();
    }

    init = () =>
    {
        this.#addListeners();
    }

    #addListeners = () =>
    {
        this.elements.copyToClipboardButton.addEventListener(NativeEvents.Click, (e) => {
            this.#copyOutputTextToClipboard();
        });
    }

    #copyOutputTextToClipboard = () =>
    {
        const text = this.getOutputText();
        Utililties.copyTextToClipboard(text);
        
        this.#showStatusMesssage();
        setTimeout(this.#hideStatusMessage, 1000);
    }

    getOutputText = () =>
    {
        let result = '';

        for (const checkbox of this.elements.checkboxes)
        {
            result += `${checkbox.getOutputText()}\n`;
        }

        return result;
    }

    #showModal = () => BootstrapUtilities.modalShow(this.elements.modal);
    #showStatusMesssage = () => this.elements.statusMessage.classList.remove(BootstrapClasses.DISPLAY_NONE);
    #hideStatusMessage = () => this.elements.statusMessage.classList.add(BootstrapClasses.DISPLAY_NONE);
}
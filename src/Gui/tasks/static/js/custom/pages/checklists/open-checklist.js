import { NativeEvents } from "../../domain/constants/native-events";
import { OpenChecklistCloseButtonClickedEvent } from "../../domain/events/events";
import { ChecklistServices } from "../../services/checklist-services";








export class OpenChecklist
{

    #isLoaded = false;
    get isLoaded()
    {
        return this.#isLoaded;
    }

    constructor(checklistId)
    {
        this.checklistId = checklistId;
        this.services = new ChecklistServices();
        this.html = null;

        /** @type {OpenChecklistElements} */
        this.elements = null;
    }


    fetchData = async () =>
    {
        this.html = await this.services.getChecklistHtml(this.checklistId);
        this.#isLoaded = true;
    }


    appendChecklistToContainer = (container) =>
    {
        $(container).append(this.html);
        this.elements = new OpenChecklistElements(this.checklistId);
        this.#addListeners();
    }

    #addListeners = () =>
    {
        this.elements.closeButton.addEventListener(NativeEvents.CLICK, (e) => 
        {
            OpenChecklistCloseButtonClickedEvent.invoke(this, this.checklistId);
        });
    }

    close = () =>
    {
        this.elements.container.remove();
    }
    

    
}




export class OpenChecklistElements 
{
    static ContainerClass = 'open-checklist-container';
    static ChecklistIdAttribute = 'data-checklist-id';

    static getSelector = (checklistId) =>
    {
        const result = `.${OpenChecklistElements.ContainerClass}[${OpenChecklistElements.ChecklistIdAttribute}="${checklistId}"]`;
        return result;
    }

    constructor(checklistId)
    {

        this.checklistId = checklistId;

        const containerSelectorText = OpenChecklistElements.getSelector(checklistId);

        /** @type {HTMLDivElement} */
        this.container = document.querySelector(containerSelectorText);

        /** @type {HTMLButtonElement} */
        this.closeButton = this.container.querySelector('.close-checklist-btn');
    }

    
}
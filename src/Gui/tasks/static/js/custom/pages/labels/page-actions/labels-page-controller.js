import { ApiLabels } from "../../../api/api-labels";
import { LabelsPageElements } from "./labels-page-elements";


export class LabelsPageController
{
    constructor()
    {
        /** @type {LabelsPageElements} */
        this.elements = new LabelsPageElements();
    }

    /**
     * Renders the labels to the page after retrieving it from the api
     */
    renderLabelsHtml = async () =>
    {
        const api = new ApiLabels();
        const response = await api.getAll();
        const data = await response.text();

        this.hideSpinner();
        this.elements.eContent.innerHTML = data;
    }


    /**
     * Hide the spinner
     */
    hideSpinner = () => this.elements.eSpinner.classList.add('d-none');
}




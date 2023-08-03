import { AlertPageTopBase, AlertPageTopDanger, AlertPageTopSuccess } from "../../components/page-alerts/alert-page-top";
import { ChecklistSettingsGeneralFormSubmittedEvent } from "../../domain/events/events";
import { UrlMethods } from "../../helpers/url-methods";
import { GeneralSettingsFormController } from "./general-settings-form-controller"



export class ChecklistSettingsPageController
{

    constructor()
    {
        this.checklistId = UrlMethods.getPathValue(1);
        this.generalSettingsForm = new GeneralSettingsFormController(this.checklistId);
    }

    init = () =>
    {
        this.generalSettingsForm.init();
        this.#addEventListeners();
    }


    #addEventListeners = () =>
    {
        ChecklistSettingsGeneralFormSubmittedEvent.addListener((e) => 
        {  
            this.#onChecklistSettingsGeneralFormSubmittedEvent(e.data);
        });
    }

    #onChecklistSettingsGeneralFormSubmittedEvent = (successful) =>
    {

        /** @type {AlertPageTopBase} */
        let customAlert = null;

        if (successful)
        {
            customAlert = new AlertPageTopSuccess('Changes saved successfully');
        }
        else
        {
            customAlert = new AlertPageTopDanger('There was an error saving the checklist. Please try again');
        }

        customAlert.show();
    }
}
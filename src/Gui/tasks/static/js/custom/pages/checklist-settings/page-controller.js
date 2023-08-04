import { AlertPageTopBase, AlertPageTopDanger, AlertPageTopSuccess } from "../../components/page-alerts/alert-page-top";
import { ChecklistSettingsChecklistClonedEvent, ChecklistSettingsChecklistDeletedEvent, ChecklistSettingsGeneralFormSubmittedEvent } from "../../domain/events/events";
import { UrlMethods } from "../../helpers/url-methods";
import { CloneChecklistController } from "./clone-checklist-controller";
import { DeleteChecklistController } from "./delete-checklist-controller";
import { GeneralSettingsFormController } from "./general-settings-form-controller"


export class ChecklistSettingsPageController
{

    constructor()
    {
        this.checklistId = UrlMethods.getPathValue(1);
        this.generalSettingsForm = new GeneralSettingsFormController(this.checklistId);
        this.deleteChecklistController = new DeleteChecklistController(this.checklistId);
        this.cloneChecklistController = new CloneChecklistController(this.checklistId);
    }

    init = () =>
    {
        this.generalSettingsForm.init();
        this.deleteChecklistController.init();
        this.cloneChecklistController.init();
        this.#addEventListeners();
    }


    #addEventListeners = () =>
    {
        ChecklistSettingsGeneralFormSubmittedEvent.addListener((e) => 
        {  
            this.#onChecklistSettingsGeneralFormSubmittedEvent(e.data);
        });

        ChecklistSettingsChecklistDeletedEvent.addListener((e) => 
        {
            window.location.href = '/checklists';
        });

        ChecklistSettingsChecklistClonedEvent.addListener((e) => 
        {
            const successAlert = new AlertPageTopSuccess("Checklist was successfully cloned");
            successAlert.show();
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
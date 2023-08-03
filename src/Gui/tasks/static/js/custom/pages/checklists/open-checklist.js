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
    }


    fetchData = async () =>
    {
        this.html = await this.services.getChecklistHtml(this.checklistId);
        this.#isLoaded = true;
    }
}
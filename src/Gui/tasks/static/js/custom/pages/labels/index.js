import { EditLabelPageFormController } from "./label-form/edit-label-form-controller";
import { NewLabelPageFormController } from "./label-form/new-label-form-controller";
import { LabelsListItemController } from "./labels-list-item/labels-list-item-controller";
import { LabelsPageController } from "./page-actions/labels-page-controller";


const m_newLabelFormController = new NewLabelPageFormController();
const m_editLabelFormController = new EditLabelPageFormController();
const m_pageController = new LabelsPageController();

const m_listItemController = new LabelsListItemController();


/**
 * Main logic
 */
$(document).ready(function() 
{
    m_pageController.renderLabelsHtml();
    m_listItemController.addListeners();
});

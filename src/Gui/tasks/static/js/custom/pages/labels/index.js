import { EditLabelPageFormController } from "./label-form/edit-label-form-controller";
import { NewLabelPageFormController } from "./label-form/new-label-form-controller";
import { LabelsPageController } from "./page-actions/labels-page-controller";


const m_newLabelFormController = new NewLabelPageFormController();
const m_editLabelFormController = new EditLabelPageFormController();
const m_pageController = new LabelsPageController();


/**
 * Main logic
 */
$(document).ready(function() 
{
    
    m_pageController.renderLabelsHtml();

});

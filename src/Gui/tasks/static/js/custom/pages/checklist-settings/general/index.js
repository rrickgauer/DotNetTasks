import { ChecklistSettingsPageController } from "./page-controller";


$(document).ready(function() 
{
    setup();
});



async function setup()
{
    const pageController = new ChecklistSettingsPageController();
    pageController.init();
}
import { LabelsPageController } from "./labels-page-controller";




$(document).ready(function() 
{
    setup();
});


function setup()
{
    const pageController = new LabelsPageController();
    pageController.init();

    console.log(pageController);
}

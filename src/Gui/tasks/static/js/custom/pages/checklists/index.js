import { ChecklistsController } from "./checklists-controller";




$(document).ready(function() {
    setup();
});


function setup()
{
    const container = document.querySelector('.checklists-page-container');
    const controller = new ChecklistsController(container);
    controller.init();

    console.log({controller});
}
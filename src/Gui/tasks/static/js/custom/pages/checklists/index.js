import { SidebarController } from "./sidebar-controller";
import { ChecklistsPageUrlWrapper } from "./url-wrapper";




$(document).ready(function() {
    setup();
    test();
});


function setup()
{
    const container = document.querySelector('.checklists-page-container');
    const controller = new SidebarController(container);
    controller.init();

    console.log({controller});
}


function test()
{

    const urlWrapper = ChecklistsPageUrlWrapper.fromCurrentUrl();

    console.log(urlWrapper.getOpenChecklistIds());

}
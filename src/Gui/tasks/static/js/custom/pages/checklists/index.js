"use strict";

import { BaseEventDetail } from "../../domain/events/detail";

import { SidebarController } from "./sidebar-controller";


$(document).ready(function() {
    setup();
    test();
});


function setup()
{
    const container = document.querySelector('.checklists-page-container');
    const controller = new SidebarController(container);
    controller.init();

    // console.log({controller});
}


function test()
{


}



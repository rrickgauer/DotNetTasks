"use strict";

import { PageController } from "./page-controller";


$(document).ready(function() 
{
    setup();
});


async function setup()
{
    const pageController = new PageController();
    await pageController.init();

}


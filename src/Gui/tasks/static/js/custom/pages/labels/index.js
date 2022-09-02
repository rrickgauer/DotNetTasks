import { ApiLabels } from "../../api/api-labels";


/**
 * Main logic
 */
$(document).ready(function() {
    // alert('labels');

    addListeners();


    getLabelsHtml();

});


function addListeners()
{

}



async function getLabelsHtml()
{
    const api = new ApiLabels();
    const response = await api.get();
    const data = await response.text();

    $('#labels-content').html(data);
}
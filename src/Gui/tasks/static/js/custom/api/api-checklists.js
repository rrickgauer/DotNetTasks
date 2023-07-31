import { ApiEndpoints } from "./api-base";



export class ApiChecklists
{

    getAll = async () =>
    {
        return await fetch(ApiEndpoints.CHECKLISTS);
    }

}
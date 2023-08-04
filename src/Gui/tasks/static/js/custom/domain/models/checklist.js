

export class ChecklistModel
{
    id = null;
	title = null;
	type = null;
	createdOn = null;
	countItems = null;


    constructor(apiResponse)
    {
        const responseProperties = Object.keys(apiResponse);

        for (const modelPropertyName of Object.keys(this))
        {
            if (responseProperties.includes(modelPropertyName))
            {
                this[modelPropertyName] = apiResponse[modelPropertyName];
            }
        }
    }
}

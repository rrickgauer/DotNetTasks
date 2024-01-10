


export class ServiceUtilities
{
    /**
     * Handle a bad response
     * @param {Response} response The response to handle
     */
    static handleBadResponse = async (response) =>
    {
        if (!response.ok)
        {
            const text = await response.text();

            // console.

            console.error({text});
            throw new Error(text);
        }
    }
}
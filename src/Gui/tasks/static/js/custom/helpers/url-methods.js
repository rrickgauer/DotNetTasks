



export class UrlMethods 
{
    /**
     * Get an updated URL with the updated url search parm
     * @param {string} key the key
     * @param {object} value new value
     * @returns {URL} the updated url
     */
    static setQueryParmAndRefresh(key, value) {
        const newUrl = new URL(window.location.href);
        newUrl.searchParams.set(key, value);
        return newUrl
    }
}
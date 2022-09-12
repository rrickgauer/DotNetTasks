using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Domain.Responses
{
    public static class ResponseUtilities
    {
        /// <summary>
        /// Move the data from the source object into the destination object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="X"></typeparam>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        public static void TransferResponseData<T, X>(IBaseResponse<T> source, IBaseResponse<X> destination)
        {
            destination.Exception = source.Exception;
            destination.Successful = source.Successful;
            destination.Message = source.Message;
        }
    }
}

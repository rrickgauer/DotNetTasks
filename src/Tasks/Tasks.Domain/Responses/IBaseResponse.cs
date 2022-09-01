using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Domain.Responses
{
    interface IBaseResponse<T>
    {
        public bool Success { get; set; }
        public Exception? Exception { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
    }
}

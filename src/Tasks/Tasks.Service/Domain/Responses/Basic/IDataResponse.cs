using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Service.Domain.Responses.Basic;

public interface IDataResponse<T>
{
    public bool Successful { get; set; }
    public Exception? Exception { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;

namespace Tasks.Domain.Responses;

public class BaseResponse : IBaseResponse<User>
{
    public bool Success { get; set; }
    public Exception? Exception { get; set; }
    public string? Message { get; set; }
    public User? Data { get; set; }
}

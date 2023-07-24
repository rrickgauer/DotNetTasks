using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Service.Errors;

public class TasksException : Exception
{
    public TasksException()
    {
    }

    public TasksException(string message) : base(message)
    {
    }

    public TasksException(string message, Exception inner) : base(message, inner)
    {
    }

}

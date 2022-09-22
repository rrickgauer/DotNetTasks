using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;
using Tasks.Services.Interfaces;

namespace Tasks.Services.Implementations;

public class WpfApplicationServices : IWpfApplicationServices
{
    public User? User { get; set; }
}

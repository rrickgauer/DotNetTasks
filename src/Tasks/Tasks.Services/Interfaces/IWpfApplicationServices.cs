﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tasks.Domain.Models;

namespace Tasks.Services.Interfaces
{
    public interface IWpfApplicationServices
    {
        public User? User { get; set; }
    }
}

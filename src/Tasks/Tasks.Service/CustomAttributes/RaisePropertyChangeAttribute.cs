﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Service.CustomAttributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
public class RaisePropertyChangeAttribute : Attribute
{

}

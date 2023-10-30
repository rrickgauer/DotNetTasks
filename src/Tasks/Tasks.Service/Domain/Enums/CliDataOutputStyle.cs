using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tasks.Service.Domain.Enums;

public enum CliDataOutputStyle
{
    [Description("Standard table format")]
    Default,

    [Description("No styling")]
    None,

    [Description("Output as a markdown table")]
    Markdown,

    [Description("Return the data as a JSON string")]
    Json,
}


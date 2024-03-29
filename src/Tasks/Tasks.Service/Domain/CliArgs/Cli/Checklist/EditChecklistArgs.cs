﻿using Tasks.Service.Domain.Enums;
using static Tasks.Service.Domain.CliArgs.Cli.Contracts.ChecklistCliContracts;

namespace Tasks.Service.Domain.CliArgs.Cli.Checklist;

public class EditChecklistArgs : IChecklistCliIndex, IChecklistCliTitle
{
    public uint? CommandLineId { get; set; }
    public string? Title { get; set; }
}

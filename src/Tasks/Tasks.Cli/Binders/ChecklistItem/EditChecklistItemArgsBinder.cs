﻿using System.CommandLine;
using System.CommandLine.Binding;
using Tasks.Cli.CommandArgs.Groups;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.CliArgs.Cli.ChecklistItem;
using static Tasks.Service.Domain.CliArgs.Cli.Contracts.ChecklistItemCliContracts;

namespace Tasks.Cli.Binders.ChecklistItem;

public class EditChecklistItemArgsBinder : ArgsBinderBase<EditChecklistItemArgs>, IValueDescriptor<EditChecklistItemArgs>
{
    [CopyTo(nameof(IChecklistItemCliChecklistId.ChecklistCommandLineId))]
    public Option<uint> ChecklistIdOption { get; set; } = ChecklistItemCommandGroup.ChecklistIdOption;

    [CopyTo(nameof(IChecklistItemCliIndex.ItemCommandLineId), ParseFunctionName = nameof(BinderCasts.ParseUInt))]
    public Argument<uint?> ItemReferenceArgument { get; set; } = ChecklistItemCommandGroup.ItemReferenceArgument;

    [CopyTo(nameof(IChecklistItemCliContent.Content))]
    public Option<string?> ContentOption { get; set; } = ChecklistItemCommandGroup.ContentOption;
}


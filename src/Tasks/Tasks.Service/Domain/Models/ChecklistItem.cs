﻿using System.Text.Json.Serialization;
using Tasks.Service.CustomAttributes;
using Tasks.Service.Domain.TableView;

namespace Tasks.Service.Domain.Models;

public class ChecklistItem : ITableViewModel<ChecklistItemView, ChecklistItem>
{
    [SqlColumn("id")]
    public Guid? Id { get; set; }

    [JsonIgnore]
    [SqlColumn("checklist_id")]
    public Guid? ChecklistId { get; set; }

    [SqlColumn("content")]
    public string? Content { get; set; }

    [SqlColumn("created_on")]
    public DateTime CreatedOn { get; set; } = DateTime.Now;

    [SqlColumn("completed_on")]
    [JsonIgnore]
    public DateTime? CompletedOn { get; set; } = null;

    [SqlColumn("position")]
    public uint Position { get; set; } = 0;


    /// <summary>
    /// Flag to check if item is complete.
    /// If CompletedOn has a value (is not null), then the item is complete.
    /// Otherwise, it is incomplete.
    /// </summary>
    public bool IsComplete
    {
        get => CompletedOn != null;
        set => CompletedOn = value ? DateTime.Now : null;
    }

    public static explicit operator ChecklistItem(ChecklistItemView other)
    {
        return new()
        {
            Id          = other.Id,
            ChecklistId = other.ChecklistId,
            Content     = other.Content,
            CreatedOn   = other.CreatedOn,
            CompletedOn = other.CompletedOn,
            Position    = other.Position,
        };
    }
}

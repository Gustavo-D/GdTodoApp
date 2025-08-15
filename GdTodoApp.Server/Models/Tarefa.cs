using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GdToDoApp.Server.Model;

public partial class Tarefa
{
    public long Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public int IsCompleted { get; set; }

    public string? Category { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public long? UserId { get; set; }
    [JsonIgnore]
    public virtual Usuario? User { get; set; }
}

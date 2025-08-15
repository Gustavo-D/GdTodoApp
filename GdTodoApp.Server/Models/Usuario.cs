using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GdToDoApp.Server.Model;

public partial class Usuario
{
    public long Id { get; set; }

    public string Username { get; set; } = null!;
    [JsonIgnore]
    public string PasswordHash { get; set; } = null!;

    public DateTime CreatedAt { get; set; }
    [JsonIgnore]
    public virtual ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
}

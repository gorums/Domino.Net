using System;

namespace Domino.Net.Core.Entities;

public  class BaseEntity<T>
{
    public T? Id { get; set; } = default;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }
}

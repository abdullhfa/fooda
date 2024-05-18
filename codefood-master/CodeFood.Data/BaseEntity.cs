using System.ComponentModel.DataAnnotations;

namespace CodeFood.Data;

public abstract class BaseEntity
{
    [Required] public Guid Id { get; set; }
    [DataType(DataType.Time)] public DateTime DateAdded { get; set; }

    protected BaseEntity() => DateAdded = DateTime.UtcNow;
}
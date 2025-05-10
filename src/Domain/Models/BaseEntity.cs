using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public abstract class BaseEntity<TId>
{
    [Key]
    public TId Id { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? ModifiedAtAt { get; set; }
    public bool IsActive { get; set; } = true;
}

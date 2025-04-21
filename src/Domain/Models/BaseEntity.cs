namespace Domain.Models;

public abstract class BaseEntity<TId>
{
    public required TId Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAtAt { get; set; }
    public bool IsActive { get; set; } = true;

}
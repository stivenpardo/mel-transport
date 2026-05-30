using System.ComponentModel.DataAnnotations;

namespace MelTransport.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; protected set; }

    }
}
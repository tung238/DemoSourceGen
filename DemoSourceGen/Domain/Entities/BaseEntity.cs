using System.ComponentModel.DataAnnotations;

namespace DemoSourceGen.Domain.Entities
{
    public class BaseEntity
    {
        [MaxLength(450)]
        public string Id { get; set; } = Guid.NewGuid().ToString("n");
        public DateTimeOffset CreatedAtUtc { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAtUtc { get; set; } = DateTimeOffset.UtcNow;
        public bool IsDeleted { get; set; }
    }
}

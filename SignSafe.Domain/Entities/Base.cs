using System.Text.Json.Serialization;

namespace SignSafe.Domain.Entities
{
    public abstract class Base
    {
        public long Id { get; protected set; }
        public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; protected set; } = null;
        [JsonIgnore]
        public DateTime? DeletedAt { get; protected set; } = null;
        [JsonIgnore]
        public bool Deleted { get; protected set; } = false;

        public void SetCreatedAt()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public void SetUpdatedAt()
        {
            UpdatedAt = DateTime.UtcNow;
        }

        public void SetDeletedAt()
        {
            DeletedAt = DateTime.UtcNow;
            Deleted = true;
        }
    }
}

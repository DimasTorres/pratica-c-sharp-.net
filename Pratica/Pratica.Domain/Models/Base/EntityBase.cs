namespace Pratica.Domain.Models.Base
{
    public abstract class EntityBase
    {
        public string Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}

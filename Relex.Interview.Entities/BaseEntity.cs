namespace Relex.Interview.Entities
{
    public interface IEntity 
    {
        int Id { get; set; }
    }

    public abstract class BaseEntity : IEntity
    {
        public int Id { get; set; }
    }
}

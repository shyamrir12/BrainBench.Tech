namespace AdminPanelApp.Data
{
    public interface TEntity
    {
    }

    public abstract class BaseEntity : TEntity
    {
    }

    public abstract class BaseEntity<T> : BaseEntity, TEntity
    {

        public abstract T Key { get; set; }
    }
}

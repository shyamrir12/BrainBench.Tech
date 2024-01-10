namespace AdminPanelAPI.Data
{
	public interface IEntity
	{
	}

	public abstract class BaseEntity : IEntity
	{
	}

	public abstract class BaseEntity<T> : BaseEntity, IEntity
	{

		public abstract T Key { get; set; }
	}

}

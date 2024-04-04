namespace Restaurant.Entity
{
	public interface IVisitor
	{
		public IVisitorSpace GetTable();
	}
}
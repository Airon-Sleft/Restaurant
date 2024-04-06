namespace Restaurant.Entity
{
	public interface IVisitor
	{
		public IVisitorSpace GetTable();
		public bool IsNeedSomeAction();
		public bool IsWaitForBringToTable();
	}
}
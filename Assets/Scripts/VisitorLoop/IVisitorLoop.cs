using Restaurant.Entity;

namespace Restaurant.General
{
	public interface IVisitorLoop
	{
		public void OnGotExit();
		public void OnGotWaiter(Waiter waiter);
		public void SendToTable(IVisitorSpace visitorSpace);
		public void OnGotTable(IVisitorSpace visitorSpace);
		public void Update();
	}
}
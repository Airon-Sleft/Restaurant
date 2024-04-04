using Restaurant.Entity;

namespace Restaurant.General
{
	public interface IVisitorLoop
	{
		public VisitorLoop.VISITOR_STATE CurrentState { get; }
		public void OnGotExit();
		public void OnGotWaiter(Waiter waiter);
		public void SendToTable(IVisitorSpace visitorSpace);
		public void OnGotTable(IVisitorSpace visitorSpace);
		public void OnWaiterBringSomething(Waiter waiter);
		public void Update();
	}
}
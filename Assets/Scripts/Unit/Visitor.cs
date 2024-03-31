using Restaurant.General;
using UnityEngine;
namespace Restaurant.Entity
{
	public class Visitor : Unit
	{
		public enum STATE
		{
			EMPTY,
			MOVING_TO_TABLE,
			SEAT,
			MOVING_TO_EXIT,
		}
		private IVisitorLoop _visitorLoop;
		protected override void Awake()
		{
			base.Awake();
			_visitorLoop = VisitorLoopFactory.Create(this, VisitorLoopFactory.BEHAVIOR.DEFAULT);
		}
		private STATE _actualState;
		private IVisitorSpace _currentVisitorSpace;
		private void OnTriggerEnter(Collider other)
		{
			if (_actualState != STATE.EMPTY) return;
			if (other.gameObject.CompareTag("Waiter"))
			{
				_visitorLoop.OnGotWaiter(other.gameObject.GetComponent<Waiter>());
			}
			else if (other.gameObject.CompareTag("Exit"))
			{
				_visitorLoop.OnGotExit();
			}
		}
		public void OnGotTable(IVisitorSpace visitorSpace)
		{
			_visitorLoop.OnGotTable(visitorSpace);
		}
		public void SendToTable(IVisitorSpace visitorSpace)
		{
			_visitorLoop.SendToTable(visitorSpace);
		}
		public void SeatToTable(IVisitorSpace visitorSpace)
		{
			_actualState = STATE.SEAT;
			_currentVisitorSpace = visitorSpace;
			ClearDestination();
			visitorSpace.SeatVisitor(this);
		}
		public void TakeFromTable()
		{
			_actualState = STATE.EMPTY;
			_currentVisitorSpace.ClearVisitor();
		}
		private void Update()
		{
			base.CheckAnimation();
			_visitorLoop.Update();
		}
	}
}
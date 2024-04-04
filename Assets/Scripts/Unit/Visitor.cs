using Restaurant.General;
using System.Collections.Generic;
using UnityEngine;
namespace Restaurant.Entity
{
	public class Visitor : Unit,IVisitor
	{
		private IVisitorLoop _visitorLoop;
		protected override void Awake()
		{
			base.Awake();
			_visitorLoop = VisitorLoopFactory.Create(this, VisitorLoopFactory.BEHAVIOR.DEFAULT);
		}
		private IVisitorSpace _currentVisitorSpace;
		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Waiter"))
			{
				if (_visitorLoop.CurrentState != VisitorLoop.VISITOR_STATE.FREE) return;
				_visitorLoop.OnGotWaiter(other.gameObject.GetComponent<Waiter>());
			}
			else if (other.gameObject.CompareTag("Exit"))
			{
				if (_visitorLoop.CurrentState != VisitorLoop.VISITOR_STATE.GOING_TO_EXIT) return;
				_visitorLoop.OnGotExit();
			}
		}
		public void OnWaiterBringSomething(Waiter waiter)
		{
			_visitorLoop.OnWaiterBringSomething(waiter);
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
			_currentVisitorSpace = visitorSpace;
			ClearDestination();
			visitorSpace.SeatVisitor(this);
		}
		public IVisitorSpace GetTable()
		{
			return _currentVisitorSpace;
		}
		private void Update()
		{
			base.CheckAnimation();
			_visitorLoop.Update();
		}
	}
}
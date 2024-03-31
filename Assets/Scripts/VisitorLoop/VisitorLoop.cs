using Restaurant.Entity;
using UnityEngine;

namespace Restaurant.General
{
	public class VisitorLoop : IVisitorLoop
	{
		public enum VISITOR_STATE
		{
			FREE,
			FOLLOW_TO_WAITER,
			FOLLOW_TO_TABLE,
			WAIT_FOR_FOOD,
			EATING,
			WAIT_FOR_BILL,
			GOING_TO_EXIT,
		}

		private readonly Visitor _visitor;
		private Waiter _currentWaiter;
		private VISITOR_STATE _currentState;
		private IVisitorSpace _currentTable;
		public VisitorLoop(Visitor visitor)
		{
			_visitor = visitor;
		}
		public void Update()
		{
			switch (_currentState)
			{
				case VISITOR_STATE.FOLLOW_TO_WAITER:
					{
						_visitor.SetDestination(_currentWaiter.gameObject, 3.0f);
						break;
					}
				case VISITOR_STATE.WAIT_FOR_FOOD:
				case VISITOR_STATE.WAIT_FOR_BILL:
				case VISITOR_STATE.EATING:
					_currentTable.PutToVisitorPos(_visitor.gameObject);
					break;
				default:
					break;
			}
		}
		public void OnGotExit()
		{

		}
		public void OnGotWaiter(Waiter waiter)
		{
			if (_currentState == VISITOR_STATE.FREE)
			{
				_currentWaiter = waiter;
				waiter.SetVisitor(_visitor);
				_currentState = VISITOR_STATE.FOLLOW_TO_WAITER;
			}
		}
		public void SendToTable(IVisitorSpace visitorSpace)
		{
			if (_currentState != VISITOR_STATE.FOLLOW_TO_WAITER) return;
			_currentState = VISITOR_STATE.FOLLOW_TO_TABLE;
			_currentTable = visitorSpace;
			_currentWaiter.ClearVisitor();
			_currentWaiter = null;
			_visitor.SetDestination(visitorSpace.GetPos(), 1.5f);
			visitorSpace.SetState(VisitorSpace.TABLE_STATE.BUSY);
		}
		public void OnGotTable(IVisitorSpace visitorSpace)
		{
			if (_currentState != VISITOR_STATE.FOLLOW_TO_TABLE || _currentTable != visitorSpace) return;
			_currentState = VISITOR_STATE.WAIT_FOR_FOOD;
			_visitor.ClearDestination();
			_currentTable.SeatVisitor(_visitor);

		}
	}
}
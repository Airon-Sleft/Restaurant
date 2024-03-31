using UnityEngine;

namespace Restaurant.Entity
{
	public class Table : VisitorSpace
	{
		protected override void OnVisitorGotSpace(Visitor visitor)
		{
			visitor.OnGotTable(this);
		}

		protected override void OnWaiterGotSpace(Waiter waiter)
		{
			if (waiter.GetVisitor() != null && _currentState == TABLE_STATE.FREE_FOR_VISITOR)
			{
				waiter.GetVisitor().SendToTable(this);
			}
			else if (waiter.GetVisitor() == null && _currentState == TABLE_STATE.WAIT_FOR_ACTION)
			{

			}
		}
	}
}
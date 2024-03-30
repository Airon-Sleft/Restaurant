using UnityEngine;

namespace Restaurant.Entity
{
	public class Table : VisitorSpace
	{
		protected override void OnVisitorGotSpace(GameObject visitor)
		{
			throw new System.NotImplementedException();
		}

		protected override void OnWaiterGotSpace(GameObject waiter)
		{
			Manager.Instance.onPlayerGotTable(this, waiter.GetComponentInParent<IUnit>());
		}
	}
}
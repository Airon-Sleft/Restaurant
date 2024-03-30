using Restaurant.Entity;
using UnityEngine;

namespace Restaurant.Resources
{
	public class CashZone : ResourceZone
	{
		public void AddBill(IUnit targetUnit)
		{
			AddResource(Resource.RES_TYPE.BILL, targetUnit);
		}
		protected override void OnWaiterGotZone(IUnit waiter)
		{
			Manager.Instance.onWaiterGotCashZone(waiter);
		}
	}
}
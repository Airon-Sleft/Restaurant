using Restaurant.Entity;
using UnityEngine;

namespace Restaurant.Resources
{
	public class CashZone : ResourceZone
	{
		public void Awake()
		{
			_oneResourceBildTime = Manager.Instance.Config.BillBuildTime;
		}
		public void AddBill(IVisitor targetUnit)
		{
			AddResource(Resource.RES_TYPE.BILL, targetUnit);
		}
		protected override void OnWaiterGotZone(Waiter waiter)
		{
		}
	}
}
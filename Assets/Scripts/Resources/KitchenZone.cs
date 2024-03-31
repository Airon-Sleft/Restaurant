using Restaurant.Entity;
using UnityEngine;

namespace Restaurant.Resources
{
	public class KitchenZone : ResourceZone
	{
		public void AddFood(IUnit targetUnit)
		{
			AddResource(Resource.RES_TYPE.FOOD, targetUnit);
		}
		protected override void OnWaiterGotZone(IUnit waiter)
		{
			Manager.Instance.OnWaiterGotKitchen(this, waiter as Waiter);
		}
	}
}
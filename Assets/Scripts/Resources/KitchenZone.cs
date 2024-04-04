using Restaurant.Entity;
using UnityEngine;

namespace Restaurant.Resources
{
	public class KitchenZone : ResourceZone
	{
		public void Awake()
		{
			_oneResourceBildTime = Manager.Instance.Config.FoodBuildTime;
		}
		public void AddFood(IVisitor targetUnit)
		{
			AddResource(Resource.RES_TYPE.FOOD, targetUnit);
		}
		protected override void OnWaiterGotZone(Waiter waiter)
		{
		}
	}
}
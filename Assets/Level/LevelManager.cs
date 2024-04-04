using Restaurant.Entity;
using Restaurant.Resources;
using UnityEngine;

namespace Restaurant.General
{
	public class LevelManager
	{
		private KitchenZone _kitchen;
		private CashZone _cashZone;
		public readonly GameObject ExitObject;
		public LevelManager(KitchenZone kitchen, CashZone cashZone, GameObject exitObject)
		{
			_kitchen = kitchen;
			_cashZone = cashZone;
			ExitObject = exitObject;
		}
		public void MakeFood(IVisitor targetUnit)
		{
			_kitchen.AddFood(targetUnit);
		}
		public void MakeBill(IVisitor targetUnit)
		{
			_cashZone.AddBill(targetUnit);
		}
	}
}
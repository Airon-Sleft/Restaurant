﻿using Restaurant.Entity;
using Restaurant.Resources;
using System.Collections.Generic;
using UnityEngine;

namespace Restaurant.General
{
	public interface ILevelManager
	{
		public GameObject ExitObject { get; }
		public void MakeFood(IVisitor targetUnit);
		public void MakeBill(IVisitor targetUnit);
		public void EverySecondUpdate();
		public Resource GetAnyResource();
		public bool DoesAnyVisitorNeedBringToTable();
	}

	public class LevelManager : ILevelManager
	{
		private readonly KitchenZone _kitchen;
		private readonly CashZone _cashZone;
		public GameObject ExitObject { get; private set; }
		private float _lastTimeSpawnVisitor;
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
		public void EverySecondUpdate()
		{
			if (GetVisitorNeedsAction() == 0 && Manager.Instance.LevelHandler.GetFreeTableCount() > 0 && 
				Time.time - _lastTimeSpawnVisitor > Manager.Instance.Config.CDVisitorSpawn || _lastTimeSpawnVisitor == 0)
			{
				SpawnVisitor();
			}
		}
		private void SpawnVisitor()
		{
			_lastTimeSpawnVisitor = Time.time;
			Manager.Instance.LevelHandler.AddVisitor();
			Manager.Instance.Player.UpdateTask();

		}
		private int GetVisitorNeedsAction()
		{
			int count = 0;
			foreach (IVisitor oneVisitor in Manager.Instance.LevelHandler.Visitors)
			{
				if (oneVisitor.IsNeedSomeAction()) count++;
			}
			return count;
		}
		public bool DoesAnyVisitorNeedBringToTable()
		{
			foreach (IVisitor oneVisitor in Manager.Instance.LevelHandler.Visitors)
			{
				if (oneVisitor.IsWaitForBringToTable()) return true;
			}
			return false;
		}
		public Resource GetAnyResource()
		{
			Resource resKitchen = _kitchen.GetFirstResource();
			Resource resCashZone = _cashZone.GetFirstResource();
			return resKitchen ?? resCashZone;
		}
	}
}
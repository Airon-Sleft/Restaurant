using Restaurant.Entity;
using Restaurant.Resources;
using System.Collections.Generic;
using UnityEngine;
namespace Restaurant.General
{
	public class LevelCreator
	{
		public List<VisitorSpace> CreateTables()
		{
			List<VisitorSpace> tables = new List<VisitorSpace>(Manager.Instance.Config.tablesPosition.Count);
			foreach (Vector3 pos in Manager.Instance.Config.tablesPosition)
			{
				VisitorSpace oneTable = GameObject.Instantiate(Manager.Instance.Config.tablePrefab, pos, Manager.Instance.Config.tablePrefab.transform.rotation).GetComponent<VisitorSpace>();
				tables.Add(oneTable);
			}
			return tables;
		}
		public GameObject CreateVisitor()
		{
			var config = Manager.Instance.Config;
			return GameObject.Instantiate(config.visitorPrefab, config.visitorSpawn[0], config.visitorPrefab.transform.rotation); 
		}
		public KitchenZone CreateKitchen()
		{
			var config = Manager.Instance.Config;
			return GameObject.Instantiate(config.KitchenPrefab, config.KitchenPrefab.transform.position, config.KitchenPrefab.transform.rotation).GetComponent<KitchenZone>();
		}
		public CashZone CreateCashZone()
		{
			var config = Manager.Instance.Config;
			return GameObject.Instantiate(config.CashZonePrefab, config.CashZonePrefab.transform.position, config.CashZonePrefab.transform.rotation).GetComponent<CashZone>();
		}
		public GameObject CreateExit()
		{
			var config = Manager.Instance.Config;
			return GameObject.Instantiate(config.ExitPrefab, config.ExitPrefab.transform.position, config.ExitPrefab.transform.rotation);
		}
	}
}
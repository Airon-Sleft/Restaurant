using System.Collections.Generic;
using UnityEngine;
namespace Restaurant.General
{
	public class LevelCreator
	{
		public List<GameObject> CreateTables()
		{
			List<GameObject> tables = new List<GameObject>(Manager.Instance.config.tablesPosition.Count);
			foreach (Vector3 pos in Manager.Instance.config.tablesPosition)
			{
				GameObject oneTable = GameObject.Instantiate(Manager.Instance.config.tablePrefab, pos, Manager.Instance.config.tablePrefab.transform.rotation);
				tables.Add(oneTable);
			}
			return tables;
		}
		public GameObject CreateVisitor()
		{
			var config = Manager.Instance.config;
			return GameObject.Instantiate(config.visitorPrefab, config.visitorSpawn[0], config.visitorPrefab.transform.rotation); 
		}
	}
}
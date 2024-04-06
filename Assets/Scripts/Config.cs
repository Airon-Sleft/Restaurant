using System.Collections.Generic;
using UnityEngine;

namespace Restaurant.General
{
	[CreateAssetMenu(fileName = "config", menuName = "Config/Level", order = 51)]
	public class Config : ScriptableObject
	{
		public GameObject tablePrefab;
		public GameObject visitorPrefab;
		public GameObject waiterPrefab;
		public GameObject foodPrefab;
		public GameObject BillPrefab;
		public GameObject KitchenPrefab;
		public GameObject CashZonePrefab;
		public GameObject ExitPrefab;

		public GameObject[] tableMarkPrefabs;
		public List<Vector3> tablesPosition;
		public List<Vector3> visitorSpawn;

		public float FoodBuildTime;
		public float BillBuildTime;
		[Tooltip("Minimal CD between spawn Visitor in Seconds")]
		public int CDVisitorSpawn;
	}
}
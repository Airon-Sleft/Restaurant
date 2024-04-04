using Restaurant.Entity;
using Restaurant.Resources;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Restaurant.General
{
	public class LevelHandler
	{
		private List<GameObject> _tables = new List<GameObject>();
		private List<Visitor> _visitors = new ();
		private LevelCreator _levelCreator;
		private KitchenZone _kitchen;
		private CashZone _cashZone;
		private LevelManager _levelManager;
		private GameObject _exitObject;
		public LevelHandler()
		{
			_levelCreator = new LevelCreator();
			_tables = _levelCreator.CreateTables();
			_kitchen = _levelCreator.CreateKitchen();
			_cashZone = _levelCreator.CreateCashZone();
			_exitObject = _levelCreator.CreateExit();
			_levelManager = new LevelManager(_kitchen, _cashZone, _exitObject);
			AddVisitor();
		}
		public LevelManager GetLevelManager()
		{
			return _levelManager;
		}
		public void AddVisitor()
		{
			GameObject visitorObject = _levelCreator.CreateVisitor();
			_visitors.Add(visitorObject.GetComponent<Visitor>());
		}
		public void RemoveVisitor(Visitor visitor)
		{
			_visitors.Remove(visitor);
			GameObject.Destroy(visitor.gameObject);
		}
	}
}
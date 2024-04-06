using Restaurant.Entity;
using Restaurant.Resources;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Restaurant.General
{
	public interface ILevelHandler
	{
		public List<Visitor> Visitors { get; }
		public ILevelManager GetLevelManager();
		public void AddVisitor();
		public void RemoveVisitor(Visitor visitor);
		public int GetFreeTableCount();
	}

	public class LevelHandler : ILevelHandler
	{
		private List<VisitorSpace> _tables = new ();
		private List<Visitor> _visitors = new ();
		public List<Visitor> Visitors { get { return _visitors; } }
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
		}
		public ILevelManager GetLevelManager()
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
		public int GetFreeTableCount()
		{
			int count = 0;
			foreach (VisitorSpace oneSpace in _tables)
			{
				if (oneSpace.IsFree()) count++;
			}
			return count;
		}
	}
}
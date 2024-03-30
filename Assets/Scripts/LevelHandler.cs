using System.Collections.Generic;
using UnityEngine;

namespace Restaurant.General
{
	public class LevelHandler
	{
		private List<GameObject> _tables = new List<GameObject>();
		private List<GameObject> _visitors = new List<GameObject> ();
		private LevelCreator _levelCreator;
		public LevelHandler()
		{
			_levelCreator = new LevelCreator();
			_tables = _levelCreator.CreateTables();
			_visitors.Add(_levelCreator.CreateVisitor());
		}
	}
}
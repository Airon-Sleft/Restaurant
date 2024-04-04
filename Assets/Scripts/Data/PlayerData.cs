using System;
using UnityEngine;

namespace Restaurant.Data
{
	public interface IDataHandler
	{
		public object DataBase { get;  }
		public void Load();
		public void Save();
	}

	public class PlayerData
	{
		public uint Money = 0;
		public uint ServedVisitor = 0;
	}
	public class PlayerDataHandler : IDataHandler
	{
		public object DataBase { get { return Data; } }
		public PlayerData Data { get; private set; }
		private readonly IDataIO<PlayerData> _dataIO;
		public PlayerDataHandler()
		{
			_dataIO = DataIOFactory<PlayerData>.Create("gameData");
		}
		public void Load()
		{
			Data = _dataIO.Load();
		}
		public void Save()
		{
			_dataIO.Save(Data);
		}
	}
}
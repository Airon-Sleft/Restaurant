using Restaurant.Data;
using Restaurant.Entity;
using Restaurant.Resources;
using UnityEngine;

namespace Restaurant.General
{
	public class Player : IPlayer
	{
		private readonly PlayerDataHandler _playerDataHandler;
		private IPlayer.TASK _currentTask;
		private Waiter _currentWaiter;
		public Player(DataManager dataManager)
		{
			PlayerDataHandler playerData = dataManager.Get<PlayerDataHandler>();
			_playerDataHandler = playerData;
			_currentWaiter = GameObject.Find("Player").GetComponent<Waiter>(); // need safery solve
		}
		public void AddServedVisitor()
		{
			_playerDataHandler.Data.ServedVisitor++;
			_playerDataHandler.Save();
		}
		public void AddMoney(int count)
		{
			if (count < 0) return;
			_playerDataHandler.Data.Money += (uint)count;
			_playerDataHandler.Save();
			Manager.Instance.GameUIHandler.UpdateInfo();
		}
		public void UpdateTask()
		{
			Resource res = Manager.LevelManager.GetAnyResource();
			_currentTask = IPlayer.TASK.WAIT;
			if (res != null)
			{
				if (res.ResourceType == Resource.RES_TYPE.FOOD) _currentTask = IPlayer.TASK.GO_TO_KITCHEN;
				else if (res.ResourceType == Resource.RES_TYPE.BILL) _currentTask = IPlayer.TASK.GO_TO_CASH_ZONE;
			}
			if (Manager.LevelManager.DoesAnyVisitorNeedBringToTable())
			{
				_currentTask = IPlayer.TASK.GO_TO_VISITOR;
			}
			if (_currentWaiter.GetVisitor() != null) _currentTask = IPlayer.TASK.GO_TO_ANY_TABLE;
			if (!_currentWaiter.IsHandFree()) _currentTask = IPlayer.TASK.GO_TO_THE_TABLE;
			Manager.Instance.GameUIHandler.UpdateInfo();
		}
		public int GetMoney()
		{
			return (int)_playerDataHandler.Data.Money;
		}
		public IPlayer.TASK GetTask()
		{
			return _currentTask;
		}
	}
}
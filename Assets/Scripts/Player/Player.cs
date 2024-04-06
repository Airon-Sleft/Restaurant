using Restaurant.Data;

namespace Restaurant.General
{
	public class Player : IPlayer
	{
		private PlayerDataHandler _playerDataHandler;
		public Player(DataManager dataManager)
		{
			PlayerDataHandler playerData = dataManager.Get<PlayerDataHandler>();
			_playerDataHandler = playerData;
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
		}
	}
}
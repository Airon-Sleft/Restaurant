using System.Collections.Generic;

namespace Restaurant.Data
{
	public class DataManager
	{
		private List<IDataHandler> _dataHandlers = new();

		public DataManager()
		{
			_dataHandlers.Add(new PlayerDataHandler());
		}
		public void LoadAll()
		{
			foreach (IDataHandler handler in _dataHandlers)
			{
				handler.Load();
			}
		}
		public void SaveAll()
		{
			foreach (IDataHandler handler in _dataHandlers)
			{
				handler.Save();
			}
		}
		public T Get<T>() where T : class
		{
			foreach (IDataHandler handler in _dataHandlers)
			{
				if (handler is T)
				{
					return handler as T;
				}
			}
			return null;
		}
	}
}
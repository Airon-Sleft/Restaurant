using System.IO;
using UnityEngine;

namespace Restaurant.Data
{
	public class DataLocalIO<T> : IDataIO<T> where T : class, new()
	{
		private string _path;
		public DataLocalIO(string dataName)
		{
			_path = Application.persistentDataPath + "/" + dataName + ".json";
		}
		public T Load()
		{
			if (!File.Exists(_path)) { return new T(); }
			string fileData = File.ReadAllText(_path);
			return JsonUtility.FromJson<T>(fileData);
		}
		public void Save(T data)
		{
			if (!File.Exists(_path))
			{
				File.Create(_path).Dispose();
			}
			string fileData = JsonUtility.ToJson(data);
			File.WriteAllText(_path, fileData);
			return;
		}
	}
}
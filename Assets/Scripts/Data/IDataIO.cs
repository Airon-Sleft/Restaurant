using UnityEditor;

namespace Restaurant.Data
{
	public interface IDataIO<T> where T : class, new()
	{
		public T Load();
		public void Save(T data);
	}

}
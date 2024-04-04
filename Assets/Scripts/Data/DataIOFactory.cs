namespace Restaurant.Data
{
	public class DataIOFactory<T> where T : class, new()
	{
		public enum STORAGE
		{
			LOCAL,
		}
		public static IDataIO<T> Create(string dataName, STORAGE storage = STORAGE.LOCAL)
		{
			switch (storage)
			{
				case STORAGE.LOCAL:
					return new DataLocalIO<T>(dataName);
				default:
					return null;
			}
		}
	}
}
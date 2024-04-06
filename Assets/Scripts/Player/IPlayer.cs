using JetBrains.Annotations;
using Restaurant.Data;

namespace Restaurant.General
{
	public interface IPlayer
	{
		public void AddServedVisitor();
		public void AddMoney(int conut);
	}
}
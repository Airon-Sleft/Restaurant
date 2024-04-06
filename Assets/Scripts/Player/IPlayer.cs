using JetBrains.Annotations;
using Restaurant.Data;

namespace Restaurant.General
{
	public interface IPlayer
	{
		public enum TASK
		{
			WAIT,
			GO_TO_VISITOR,
			GO_TO_ANY_TABLE,
			GO_TO_KITCHEN,
			GO_TO_CASH_ZONE,
			GO_TO_THE_TABLE,
		}

		public void AddServedVisitor();
		public void AddMoney(int conut);
		public int GetMoney();
		public TASK GetTask();
		public void UpdateTask();
	}
}
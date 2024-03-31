using Restaurant.Entity;
namespace Restaurant.General
{
	public class VisitorLoopFactory
	{
		public enum BEHAVIOR
		{
			DEFAULT,
		}

		public static IVisitorLoop Create(Visitor visitor, BEHAVIOR behavior)
		{
			switch (behavior)
			{
				case BEHAVIOR.DEFAULT:
					return new VisitorLoop(visitor);
				default:
					return null;
			}
		}
	}
}
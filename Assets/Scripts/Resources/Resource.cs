using Restaurant.Entity;

namespace Restaurant.Resources
{
	public class Resource
	{
		public enum RES_TYPE
		{ 
			FOOD,
			BILL,
		}
		private RES_TYPE _resType;
		private IUnit _targetVisitor;

		public Resource(RES_TYPE resType, IUnit targetVisitor)
		{
			_resType = resType;
			_targetVisitor = targetVisitor;
		}
		public bool IsCorrectVisitor(IUnit visitor)
		{
			return visitor == _targetVisitor;
		}
	}
}
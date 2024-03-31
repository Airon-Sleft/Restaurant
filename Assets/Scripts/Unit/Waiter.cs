using Restaurant.Resources;
namespace Restaurant.Entity
{
	public class Waiter : Unit
	{
		private Resource _resInHand;
		private Visitor _handleVisitor;
		public bool IsHandFree()
		{
			return _resInHand == null;
		}
		public bool AddResource(Resource resource)
		{
			if (!IsHandFree()) return false;
			_resInHand = resource;
			return true;
		}
		public Resource GetResource()
		{
			return _resInHand;
		}
		public void ClearResource()
		{
			_resInHand = null;
		}
		public void SetVisitor(Visitor visitor)
		{
			_handleVisitor = visitor;
		}
		public void ClearVisitor()
		{
			_handleVisitor = null;
		}
		public Visitor GetVisitor()
		{
			return _handleVisitor;
		}
	}
}
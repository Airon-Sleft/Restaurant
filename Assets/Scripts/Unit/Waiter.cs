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
			animator.SetInteger("WeaponType_int", 1);
			return true;
		}
		public Resource GetResource()
		{
			return _resInHand;
		}
		public void ClearResource()
		{
			animator.SetInteger("WeaponType_int", 0);
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
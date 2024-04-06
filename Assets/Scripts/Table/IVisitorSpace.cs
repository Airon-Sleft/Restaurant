using UnityEngine;
namespace Restaurant.Entity
{
	public interface IVisitorSpace
	{
		public void SetState(VisitorSpace.TABLE_STATE state);
		public void SeatVisitor(Visitor visitor);
		public void ClearVisitor();
		public Vector3 GetPos();
		public VisitorSpace.TABLE_STATE GetState();
		public Visitor GetVisitor();
		public void PutToVisitorPos(GameObject visitorObject);
		public GameObject GetObject();
		public bool IsFree();

	}
}
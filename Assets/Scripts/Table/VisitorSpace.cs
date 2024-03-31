using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace Restaurant.Entity
{
	public abstract class VisitorSpace : MonoBehaviour, IVisitorSpace
	{
		public enum TABLE_STATE
		{
			UNDEFINED,
			WAIT_FOR_ACTION,
			FREE_FOR_VISITOR,
			BUSY,
		}
		protected TABLE_STATE _currentState;
		protected VisitorSpaceStateMark _bottomMark;
		private Visitor _currentVisitor;
		void Awake()
		{
			_bottomMark = new VisitorSpaceStateMark(Manager.Instance.Config.tableMarkPrefabs, gameObject);
			SetState(TABLE_STATE.FREE_FOR_VISITOR);
		}
		public virtual void SetState(TABLE_STATE state)
		{
			_bottomMark.SetState(state);
			_currentState = state;
		}
		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Visitor"))
			{
				OnVisitorGotSpace(other.gameObject.GetComponent<Visitor>());
			}
			else if (other.gameObject.CompareTag("Waiter"))
			{
				OnWaiterGotSpace(other.gameObject.GetComponent<Waiter>());
			}
		}
		protected abstract void OnWaiterGotSpace(Waiter waiter);
		protected abstract void OnVisitorGotSpace(Visitor visitor);

		public virtual void SeatVisitor(Visitor visitor)
		{
			SetState(TABLE_STATE.BUSY);
			_currentVisitor = visitor;
			PutToVisitorPos(visitor.gameObject);
		}
		public virtual void ClearVisitor()
		{
			SetState(TABLE_STATE.FREE_FOR_VISITOR);
			_currentVisitor = null;
		}
		public Vector3 GetPos()
		{
			return transform.position;
		}
		public void PutToVisitorPos(GameObject visitorObject)
		{
			Vector3 pos = gameObject.transform.TransformPoint(new Vector3(0, 0, -1.0f));
			visitorObject.transform.SetPositionAndRotation(pos,  Quaternion.Euler(0, 0, 0));			
		}
		public TABLE_STATE GetState()
		{
			return _currentState;
		}
		public Visitor GetVisitor()
		{
			return _currentVisitor;
		}
	}
}
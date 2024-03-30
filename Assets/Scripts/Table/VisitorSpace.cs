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
		void Awake()
		{
			_bottomMark = new VisitorSpaceStateMark(Manager.Instance.config.tableMarkPrefabs, gameObject);
			_bottomMark.SetState(TABLE_STATE.FREE_FOR_VISITOR);
		}
		public virtual void SetState(TABLE_STATE state)
		{
			_bottomMark.SetState(state);
		}
		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Visitor"))
			{
				OnVisitorGotSpace(other.gameObject);
			}
			else if (other.gameObject.CompareTag("Waiter"))
			{
				OnWaiterGotSpace(other.gameObject);
			}
		}
		protected abstract void OnWaiterGotSpace(GameObject waiter);
		protected abstract void OnVisitorGotSpace(GameObject visitor);
	}
}
using Restaurant.Entity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Restaurant.Resources
{
	public abstract class ResourceZone : MonoBehaviour, IResourceZone
	{
		protected List<Resource> _resources = new List<Resource>();
		private List<GameObject> _inTriggerNow = new();
		private List<Resource> _queueOfResources = new();
		private float _lastBuildTime = 0.0f;
		protected float _oneResourceBildTime = 1.0f;
		protected void AddResource(Resource.RES_TYPE resType, IVisitor targetUnit, bool isImmideately = false)
		{
			Resource res = new Resource(resType, targetUnit, gameObject);
			if (isImmideately)
			{
				_resources.Add(res);
			}
			else
			{
				_queueOfResources.Add(res);
				res.SetObjectVisible(false);
			}
		}
		private void OnTriggerEnter(Collider other)
		{
			if (_inTriggerNow.Contains(other.gameObject)) return; // stop duplicate trigger (for example due the NavMeshAgent collider)
			if (other.gameObject.CompareTag("Waiter"))
			{
				Waiter waiter = other.gameObject.GetComponent<Waiter>();
				TryToGiveResource(waiter);
				OnWaiterGotZone(waiter);
				_inTriggerNow.Add(other.gameObject);
			}
		}
		private void OnTriggerExit(Collider other)
		{
			_inTriggerNow.Remove(other.gameObject);
		}
		public Resource GetFirstResource()
		{
			return _resources.FirstOrDefault();
		}
		protected abstract void OnWaiterGotZone(Waiter waiter);
		private void TryToGiveResource(Waiter waiter)
		{
			if (!waiter.IsHandFree()) return;
			if (_resources.Count == 0) return;
			Resource res = _resources.FirstOrDefault();
			_resources.Remove(res);
			waiter.AddResource(res);
			var table = res.TargetVisitor.GetTable();
			table.SetState(VisitorSpace.TABLE_STATE.WAIT_FOR_ACTION);
			res.ChangeParent(waiter.gameObject, Resource.PARENT_TYPE.WAITER);
			Manager.Instance.Player.UpdateTask();
		}
		void Update()
		{
			if (_queueOfResources.Count > 0)
			{
				if (Time.time - _lastBuildTime >= _oneResourceBildTime)
				{
					Resource res = _queueOfResources.FirstOrDefault();
					res.SetObjectVisible(true);
					_queueOfResources.Remove(res);
					_resources.Add(res);
					_lastBuildTime = Time.time;
					Manager.Instance.Player.UpdateTask();
				}
			}
			else
			{
				_lastBuildTime = Time.time;
			}
		}
	}
}
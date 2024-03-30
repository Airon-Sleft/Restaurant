using Restaurant.Entity;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Restaurant.Resources
{
	public abstract class ResourceZone : MonoBehaviour, IResourceZone
	{
		protected List<Resource> _resources = new List<Resource>();

		protected void AddResource(Resource.RES_TYPE resType, IUnit targetUnit)
		{
			Resource res = new Resource(resType, targetUnit);
			_resources.Add(res);
		}
		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Waiter"))
			{
				OnWaiterGotZone(other.gameObject.GetComponent<IUnit>());
			}
		}
		public Resource TakeIfPossible()
		{
			if (_resources.Count > 0)
			{
				Resource res = _resources.FirstOrDefault();
				_resources.Remove(res);
				return res;
			}
			return null;
		}
		protected abstract void OnWaiterGotZone(IUnit waiter);
	}
}
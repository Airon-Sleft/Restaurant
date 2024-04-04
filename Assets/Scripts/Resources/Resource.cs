using Restaurant.Entity;
using UnityEngine;

namespace Restaurant.Resources
{
	public class Resource
	{
		public enum RES_TYPE
		{ 
			FOOD,
			BILL,
		}
		public enum PARENT_TYPE
		{
			WAITER,
			KITCHEN,
			CASH_ZONE,
			VISITOR_TABLE,
		}

		private RES_TYPE _resType;
		public IVisitor TargetVisitor { get; private set; }
		private GameObject _resourceObject;

		public Resource(RES_TYPE resType, IVisitor targetVisitor, GameObject parent)
		{
			_resType = resType;
			TargetVisitor = targetVisitor;
			GameObject prefab = GetPrefabByType();
			if (prefab != null )
			{
				_resourceObject = GameObject.Instantiate(prefab, parent.transform, false);
				ChangeParent(parent, PARENT_TYPE.KITCHEN);
			}
		}
		public bool IsCorrectVisitor(IVisitor visitor)
		{
			return visitor == TargetVisitor;
		}
		public void SetObjectVisible(bool state)
		{
			if (_resourceObject == null) return;
			_resourceObject.SetActive(state);
		}
		public void ChangeParent(GameObject parent, PARENT_TYPE pType)
		{
			Vector3 offset = Vector3.zero;
			_resourceObject.transform.SetParent(parent.transform, false);
			switch(pType)
			{
				case PARENT_TYPE.KITCHEN:
				case PARENT_TYPE.CASH_ZONE:
				case PARENT_TYPE.VISITOR_TABLE:
					offset = new Vector3(0, parent.GetComponent<MeshRenderer>().bounds.size.y / 2 + 0.01f, 0.0f);
					break;
				case PARENT_TYPE.WAITER:
					offset = new Vector3(0, 1.123f, 0.89f);
					break;
				default:
					break;
			};
			_resourceObject.transform.localPosition = offset;
			_resourceObject.transform.localScale = (new Vector3(1 / parent.transform.localScale.x, 1 / parent.transform.localScale.y, 1 / parent.transform.localScale.z));
		}
		public void DestroyObject()
		{
			if (_resourceObject != null) GameObject.Destroy(_resourceObject);
		}
		private GameObject GetPrefabByType()
		{
			switch (_resType)
			{
				case RES_TYPE.FOOD:
					return Manager.Instance.Config.foodPrefab;
				case RES_TYPE.BILL:
					return Manager.Instance.Config.BillPrefab;
				default:
					return null;
			}
		}
	}
}
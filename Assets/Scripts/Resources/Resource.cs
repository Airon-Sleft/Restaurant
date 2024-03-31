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
		private RES_TYPE _resType;
		private IUnit _targetVisitor;
		private GameObject _resourceObject;

		public Resource(RES_TYPE resType, IUnit targetVisitor, GameObject parent)
		{
			_resType = resType;
			_targetVisitor = targetVisitor;
			GameObject prefab = GetPrefabByType();
			if (prefab != null )
			{
				_resourceObject = GameObject.Instantiate(prefab, parent.transform.position + new Vector3(0, parent.GetComponent<MeshRenderer>().bounds.size.y / 2 + 0.01f, 0.0f), Quaternion.identity);
			}
		}
		public bool IsCorrectVisitor(IUnit visitor)
		{
			return visitor == _targetVisitor;
		}
		public void SetObjectVisible(bool state)
		{
			_resourceObject?.SetActive(state);
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
				default:
					return null;
			}
		}
	}
}
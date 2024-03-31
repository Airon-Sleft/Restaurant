using UnityEngine;
namespace Restaurant.Entity
{
	public interface IUnit
	{
		public void SetDestination(Vector3 destination, float distanceStop = 1.0f);
		public void SetDestination(GameObject objectToFollow, float distanceStop = 1.0f);
		public Vector3 GetPos();
	}
}
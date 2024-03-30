using UnityEngine;
namespace Restaurant.Entity
{
	public class Visitor : Unit
	{
		private void OnTriggerEnter(Collider other)
		{
			if (other.gameObject.CompareTag("Waiter"))
			{
				Manager.Instance.onPlayerGotVisitor(this, other.gameObject.GetComponent<Waiter>());
			}
		}
	}
}
using Restaurant.Entity;
using UnityEngine;
using UnityEngine.AI;

public abstract class Unit : MonoBehaviour, IUnit
{
	protected Animator animator;
	private UnityMove _uMove;
	protected virtual void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		_uMove = new UnityMove(gameObject.GetComponent<NavMeshAgent>());
	}
	public void SetDestination(GameObject objectToFollow, float distanceStop = 1.0f)
	{
		SetDestination(objectToFollow.transform.position, distanceStop);
	}
	public void SetDestination(Vector3 destination, float distanceStop = 1.0f)
	{
		_uMove.SetDestination(destination, distanceStop);
	}
	public void ClearDestination()
	{
		_uMove.ClearDestination();
	}
	private void Update()
	{
		CheckAnimation();
	}
	protected void CheckAnimation()
	{
        if (_uMove.GetSpeed() > 0.2f)
        {
			animator.SetFloat("Speed_f", 0.3f);
        }
		else
		{
			animator.SetFloat("Speed_f", 0);
		}
	}
	public Vector3 GetPos()
	{
		return transform.position;
	}
}
using Restaurant.Entity;
using UnityEngine;
using UnityEngine.AI;

public abstract class Unit : MonoBehaviour, IUnit
{
	private Animator animator;
	private UnityMove _uMove;
	public void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		_uMove = new UnityMove(gameObject.GetComponent<NavMeshAgent>());
	}
	public void SetDestination(Vector3 destination)
	{
		_uMove.SetDistination(destination);
	}
	private void Update()
	{
		_uMove.Update();
		CheckAnimation();
	}
	private void CheckAnimation()
	{
        if ( _uMove.GetSpeed() > 1.0f)
        {
			animator.SetFloat("Speed_f", 0.3f);
        }
		else
		{
			animator.SetFloat("Speed_f", 0);
		}
	}
}
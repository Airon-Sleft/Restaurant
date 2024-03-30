using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour
{
	private Animator animator;
	private Rigidbody rb;
	private NavMeshAgent m_Agent;
	public void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		rb = GetComponent<Rigidbody>();
		m_Agent = GetComponent<NavMeshAgent>();
	}
	public void SetDestination(Vector3 destination)
	{
		m_Agent.SetDestination(destination);
	}
	private void Update()
	{
        if ( m_Agent.velocity.magnitude > 1)
        {
			animator.SetFloat("Speed_f", 0.3f);
        }
		else
		{
			animator.SetFloat("Speed_f", 0);

		}
	}
}
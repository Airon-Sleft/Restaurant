using UnityEngine;
using UnityEngine.AI;

namespace Restaurant.Entity
{
	public class UnityMove
	{
		private NavMeshAgent m_Agent;
		public UnityMove(NavMeshAgent agent)
		{
			m_Agent = agent;
		}
		public void SetDistination(Vector3 destination)
		{
			m_Agent.SetDestination(destination);
		}
		public void Update()
		{
			if (m_Agent.hasPath && m_Agent.remainingDistance < 1.5)
			{
				m_Agent.ResetPath();
			}
		}
		public float GetSpeed()
		{
			return m_Agent.velocity.magnitude;
		}
	}
}
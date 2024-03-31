using UnityEngine;
using UnityEngine.AI;

namespace Restaurant.Entity
{
	public class UnityMove
	{
		private NavMeshAgent m_Agent;
		private bool _needAutoClear;
		public UnityMove(NavMeshAgent agent)
		{
			m_Agent = agent;
		}
		public void SetDestination(GameObject gameObjectToFollow, float stoppingDistance = 1.0f)
		{
			SetDestination(gameObjectToFollow.transform.position, stoppingDistance);
		}
		public void SetDestination(Vector3 destination, float stoppingDistance = 1.5f)
		{
			m_Agent.SetDestination(destination);
			m_Agent.stoppingDistance = stoppingDistance;
		}
		public void ClearDestination()
		{
			m_Agent.ResetPath();
		}
		public float GetSpeed()
		{
			return m_Agent.velocity.magnitude;
		}
	}
}
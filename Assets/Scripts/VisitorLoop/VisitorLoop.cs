using Restaurant.Entity;
using Restaurant.Resources;
using Unity.VisualScripting;
using UnityEngine;

namespace Restaurant.General
{
	public class VisitorLoop : IVisitorLoop
	{
		public enum VISITOR_STATE
		{
			FREE,
			FOLLOW_TO_WAITER,
			FOLLOW_TO_TABLE,
			WAIT_FOR_FOOD,
			EATING,
			WAIT_FOR_BILL,
			GOING_TO_EXIT,
		}

		private readonly Visitor _visitor;
		private Waiter _currentWaiter;
		private Resource _currentResource;
		private VISITOR_STATE _currentState;
		public VISITOR_STATE CurrentState
		{
			get { return _currentState; }
		}
		private IVisitorSpace _currentTable;
		private float _timeForNewState = 0.0f;
		private bool _isStateWaitTime = false;
		public VisitorLoop(Visitor visitor)
		{
			_visitor = visitor;
		}
		public void Update()
		{
			switch (_currentState)
			{
				case VISITOR_STATE.FOLLOW_TO_WAITER:
					{
						_visitor.SetDestination(_currentWaiter.gameObject, 3.0f);
						break;
					}
				case VISITOR_STATE.WAIT_FOR_FOOD:
				case VISITOR_STATE.WAIT_FOR_BILL:
				case VISITOR_STATE.EATING:
					_currentTable.PutToVisitorPos(_visitor.gameObject);
					break;
				case VISITOR_STATE.GOING_TO_EXIT:
					{
						_visitor.SetDestination(Manager.LevelManager.ExitObject);
						break;
					}
				default:
					break;
			}
			if (_isStateWaitTime && Time.time >= _timeForNewState)
			{
				_isStateWaitTime = false;
				OnStateChange();
			}
		}
		public void OnGotExit()
		{
			Manager.Instance.LevelHandler.RemoveVisitor(_visitor);
			Manager.Instance.LevelHandler.AddVisitor();
		}
		public void OnGotWaiter(Waiter waiter)
		{
			if (_currentState == VISITOR_STATE.FREE)
			{
				waiter.ClearVisitor();
				_currentWaiter = waiter;
				waiter.SetVisitor(_visitor);
				_currentState = VISITOR_STATE.FOLLOW_TO_WAITER;
			}
		}
		public void SendToTable(IVisitorSpace visitorSpace)
		{
			if (_currentState != VISITOR_STATE.FOLLOW_TO_WAITER) return;
			_currentState = VISITOR_STATE.FOLLOW_TO_TABLE;
			_currentTable = visitorSpace;
			_currentWaiter.ClearVisitor();
			_currentWaiter = null;
			_visitor.SetDestination(visitorSpace.GetPos(), 0.5f);
			visitorSpace.SetState(VisitorSpace.TABLE_STATE.BUSY);
		}
		public void OnGotTable(IVisitorSpace visitorSpace)
		{
			if (_currentState != VISITOR_STATE.FOLLOW_TO_TABLE || _currentTable != visitorSpace) return;
			_currentState = VISITOR_STATE.WAIT_FOR_FOOD;
			_visitor.SeatToTable(visitorSpace);
			_currentTable.SeatVisitor(_visitor);
			Manager.LevelManager.MakeFood(_visitor);
		}
		public void OnWaiterBringSomething(Waiter waiter)
		{
			_currentTable.SetState(VisitorSpace.TABLE_STATE.BUSY);
			Resource res = waiter.GetResource();
			_currentResource = res;
			if (res.IsCorrectVisitor(_visitor))
			{
				waiter.ClearResource();
				res.ChangeParent(_currentTable.GetObject(), Resource.PARENT_TYPE.VISITOR_TABLE);
				if (_currentState == VISITOR_STATE.WAIT_FOR_FOOD)
				{
					_currentState = VISITOR_STATE.EATING;
					SetTransition(Random.Range(2.5f, 4.5f));
				}
				else if (_currentState == VISITOR_STATE.WAIT_FOR_BILL)
				{
					res.DestroyObject();
					_currentTable.ClearVisitor();
					_currentState = VISITOR_STATE.GOING_TO_EXIT;
				}
			}
		}
		private void SetTransition(float transitionTime)
		{
			_isStateWaitTime = true;
			_timeForNewState = Time.time + transitionTime;
		}
		public void OnStateChange()
		{
			if (_currentState == VISITOR_STATE.EATING)
			{
				Manager.LevelManager.MakeBill(_visitor);
				_currentResource.DestroyObject();
				_currentResource = null;
				_currentState = VISITOR_STATE.WAIT_FOR_BILL;
			}
		}
	}
}
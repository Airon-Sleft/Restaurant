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
				case VISITOR_STATE.FOLLOW_TO_TABLE:
					{
						_visitor.SetDestination(_currentTable.GetPos(), 0.5f);
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
				ChangeState();
			}
		}
		public void OnGotExit()
		{
			Manager.Instance.LevelHandler.RemoveVisitor(_visitor);
			Manager.Instance.Player.AddServedVisitor();

		}
		public void OnGotWaiter(Waiter waiter)
		{
			if (_currentState == VISITOR_STATE.FREE)
			{
				_currentWaiter = waiter;
				ChangeState();
			}
		}
		public void SendToTable(IVisitorSpace visitorSpace)
		{
			if (_currentState != VISITOR_STATE.FOLLOW_TO_WAITER) return;
			_currentTable = visitorSpace;
			ChangeState();
		}
		public void OnGotTable(IVisitorSpace visitorSpace)
		{
			if (_currentState != VISITOR_STATE.FOLLOW_TO_TABLE || _currentTable != visitorSpace) return;
			ChangeState();
		}
		public void OnWaiterBringSomething(Waiter waiter)
		{
			Resource res = waiter.GetResource();
			if (res.IsCorrectVisitor(_visitor))
			{
				_currentTable.SetState(VisitorSpace.TABLE_STATE.BUSY);
				_currentResource = res;
				_currentResource.ChangeParent(_currentTable.GetObject(), Resource.PARENT_TYPE.VISITOR_TABLE);
				waiter.ClearResource();
				ChangeState();
			}
		}
		private void SetTransition(float transitionTime)
		{
			_isStateWaitTime = true;
			_timeForNewState = Time.time + transitionTime;
		}
		public void ChangeState()
		{
			if (_currentState == VISITOR_STATE.FREE)
			{
				_currentWaiter.ClearVisitor();
				_currentWaiter.SetVisitor(_visitor);
				_currentState = VISITOR_STATE.FOLLOW_TO_WAITER;
			}
			else if (_currentState == VISITOR_STATE.FOLLOW_TO_WAITER)
			{
				_currentState = VISITOR_STATE.FOLLOW_TO_TABLE;
				_currentWaiter.ClearVisitor();
				_currentWaiter = null;
				_currentTable.SetState(VisitorSpace.TABLE_STATE.BUSY);
			}
			else if (_currentState == VISITOR_STATE.FOLLOW_TO_TABLE)
			{
				_currentState = VISITOR_STATE.WAIT_FOR_FOOD;
				_visitor.SeatToTable(_currentTable);
				_currentTable.SeatVisitor(_visitor);
				Manager.LevelManager.MakeFood(_visitor);
			}
			else if (_currentState == VISITOR_STATE.WAIT_FOR_FOOD)
			{
				_currentState = VISITOR_STATE.EATING;
				SetTransition(Random.Range(2.5f, 4.5f));
			}
			else if (_currentState == VISITOR_STATE.EATING)
			{
				Manager.LevelManager.MakeBill(_visitor);
				_currentResource.DestroyObject();
				_currentResource = null;
				_currentState = VISITOR_STATE.WAIT_FOR_BILL;
			}
			else if (_currentState == VISITOR_STATE.WAIT_FOR_BILL)
			{
				_currentResource.ChangeParent(_currentTable.GetObject(), Resource.PARENT_TYPE.VISITOR_TABLE);
				_currentResource.DestroyObject();
				_currentTable.ClearVisitor();
				_currentState = VISITOR_STATE.GOING_TO_EXIT;
				Manager.Instance.Player.AddMoney(Random.Range(5, 15));
			}
			Manager.Instance.Player.UpdateTask();
		}
	}
}
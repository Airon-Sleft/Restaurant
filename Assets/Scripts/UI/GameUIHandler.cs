using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Restaurant.General;

public class GameUIHandler : MonoBehaviour
{
	public TMP_Text moneyInfo;
	public TMP_Text taskInfo;
	private static Dictionary<IPlayer.TASK, string> _taskDict  = new() 
	{
		{IPlayer.TASK.WAIT, "Ожидайте" },
		{IPlayer.TASK.GO_TO_VISITOR, "Идите к гостю" },
		{IPlayer.TASK.GO_TO_ANY_TABLE, "Отведите гостя к любому свободному столу" },
		{IPlayer.TASK.GO_TO_THE_TABLE, "Идите к отмеченному столу" },
		{IPlayer.TASK.GO_TO_KITCHEN, "Заберите еду с кухни" },
		{IPlayer.TASK.GO_TO_CASH_ZONE, "Заберите счёт с кассы" },
	};
	public void ChangeMoney(int count)
	{
		moneyInfo.SetText(count + " $");
	}
	public void ChangeTask(IPlayer.TASK task)
	{
		if (_taskDict.ContainsKey(task))
		{
			taskInfo.SetText(_taskDict[task]);
		}
	}
	public void UpdateInfo()
	{
		ChangeMoney(Manager.Instance.Player.GetMoney());
		ChangeTask(Manager.Instance.Player.GetTask());
	}
}

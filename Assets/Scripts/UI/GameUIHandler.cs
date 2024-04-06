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
		{IPlayer.TASK.WAIT, "��������" },
		{IPlayer.TASK.GO_TO_VISITOR, "����� � �����" },
		{IPlayer.TASK.GO_TO_ANY_TABLE, "�������� ����� � ������ ���������� �����" },
		{IPlayer.TASK.GO_TO_THE_TABLE, "����� � ����������� �����" },
		{IPlayer.TASK.GO_TO_KITCHEN, "�������� ��� � �����" },
		{IPlayer.TASK.GO_TO_CASH_ZONE, "�������� ���� � �����" },
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

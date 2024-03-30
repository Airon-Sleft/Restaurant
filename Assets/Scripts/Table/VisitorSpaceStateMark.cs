using System.Collections.Generic;
using UnityEngine;

namespace Restaurant.Entity
{
	public class VisitorSpaceStateMark
	{
		private List<GameObject> _markObjects = new ();
		private GameObject _visitorSpace;
		private VisitorSpace.TABLE_STATE _actualState;
		public VisitorSpaceStateMark(GameObject[] markPrefabs, GameObject visitorSpace)
		{
			_visitorSpace = visitorSpace;
			_markObjects.Capacity = markPrefabs.Length;
			for (int i = 0; i < markPrefabs.Length; i++)
			{
				GameObject gameObject = GameObject.Instantiate(markPrefabs[i]);
				gameObject.transform.Rotate(new Vector3(90, 0, 0));
				gameObject.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
				gameObject.SetActive(false);
				gameObject.transform.SetParent(_visitorSpace.transform, false);
				_markObjects.Add(gameObject);
			}
		}
		public void SetState(VisitorSpace.TABLE_STATE state)
		{
			TurnOffActive();
			_actualState = state;
			SetMark(true);
		}
		private void TurnOffActive()
		{
			if (_actualState == VisitorSpace.TABLE_STATE.UNDEFINED) return;
			SetMark(false);
			_actualState = VisitorSpace.TABLE_STATE.UNDEFINED;
		}

		private void SetMark(bool state)
		{
			int index = (int)_actualState-1;
			if (index >= _markObjects.Count) return;
			_markObjects[index].SetActive(state);
		}
	}
}
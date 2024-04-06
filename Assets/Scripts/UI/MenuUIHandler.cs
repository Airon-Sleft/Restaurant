using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace Restaurant.General
{
	public class MenuUIHandler : MonoBehaviour
	{
		public TMP_InputField InputField;

		public void StartTheGame()
		{
			GameManager.Instance.SetPlayerName(InputField.text);
			GameManager.SceneManager.Load(ISceneManager.SCENE.GAME_LEVEL, LoadSceneMode.Single);
		}
		public void ExitTheGame()
		{
			GameManager.Instance.ExitTheGame();
		}
	}
}


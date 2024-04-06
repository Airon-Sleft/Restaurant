using UnityEngine;

namespace Restaurant.General
{
	public class GameManager : MonoBehaviour
	{
		public static GameManager Instance;
		public static ISceneManager SceneManager;
		public string PlayerName {  get; private set; }
		private void Awake()
		{
			if (Instance != null)
			{
				Destroy(gameObject);
				return;
			}
			Instance = this;
			DontDestroyOnLoad(gameObject);
			SceneManager = new OwnSceneManager();
		}
		public void SetPlayerName(string playerName)
		{
			PlayerName = playerName;
		}
		public void ExitTheGame()
		{
#if UNITY_EDITOR
			UnityEditor.EditorApplication.ExitPlaymode();
#else
		Application.Quit();	
#endif
		}
	}
}
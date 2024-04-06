using UnityEngine.SceneManagement;

namespace Restaurant.General
{
	public interface ISceneManager
	{
		public enum SCENE 
		{
			MAIN_MENU,
			GAME_LEVEL,
		}
		public void Load(SCENE scene, LoadSceneMode mode = LoadSceneMode.Single);
		public void Unload(SCENE scene);
	}
}
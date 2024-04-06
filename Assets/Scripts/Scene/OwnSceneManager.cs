using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Restaurant.General
{
	public class OwnSceneManager : ISceneManager
	{
		private readonly Dictionary<ISceneManager.SCENE, int> _sceneDict = new()
		{
			{ISceneManager.SCENE.MAIN_MENU, 0 },
			{ISceneManager.SCENE.GAME_LEVEL, 1 },
		};
		public void Load(ISceneManager.SCENE scene, LoadSceneMode mode = LoadSceneMode.Single)
		{
			if (!_sceneDict.ContainsKey(scene)) return;
			SceneManager.LoadScene(_sceneDict[scene], mode);
		}
		public void Unload(ISceneManager.SCENE scene)
		{
			if (!_sceneDict.ContainsKey(scene)) return;
			SceneManager.UnloadSceneAsync(_sceneDict[scene]);
		}
	}
}
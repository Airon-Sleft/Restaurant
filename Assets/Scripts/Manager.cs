using Restaurant.Data;
using Restaurant.Entity;
using Restaurant.General;
using Restaurant.Resources;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private Config config;
    public Config Config {  get { return config; } }
    public static Manager Instance { get; private set; }
    public static ILevelManager LevelManager { get; private set; }
    public ILevelHandler LevelHandler { get; private set; }
    private float _lastUpdateTime;

    public IPlayer Player { get; private set; }
    private DataManager _dataManager;
    public GameUIHandler GameUIHandler { get; private set; }

    void Awake()
    {
        Instance = this;
        GameUIHandler = GameObject.Find("Canvas").GetComponent<GameUIHandler>();
        LevelHandler = new LevelHandler();
        LevelManager = LevelHandler.GetLevelManager();
        _dataManager = new DataManager();
        _dataManager.LoadAll();
        Player = new Player(_dataManager);
		GameUIHandler.UpdateInfo();
	}

	private void Update()
	{
		if (Time.time - _lastUpdateTime >= 1.0f)
        {
            _lastUpdateTime = Time.time;
            LevelManager.EverySecondUpdate();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameManager.Instance.ExitTheGame();
            //if (_isGameActive)
            //{
            //    _isGameActive = false;
            //    Time.timeScale = 0.0f;
            //    GameManager.SceneManager.Load(ISceneManager.SCENE.MAIN_MENU, UnityEngine.SceneManagement.LoadSceneMode.Additive);
            //}
            //else
            //{
            //    _isGameActive = true;
            //    Time.timeScale = 1.0f;
            //    GameManager.SceneManager.Unload(ISceneManager.SCENE.MAIN_MENU);
            //}
        }
	}
}

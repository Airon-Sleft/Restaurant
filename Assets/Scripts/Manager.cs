using Restaurant.Data;
using Restaurant.Entity;
using Restaurant.General;
using Restaurant.Resources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField] private Config config;
    public Config Config {  get { return config; } }
    public static Manager Instance { get; private set; }
    public static ILevelManager LevelManager { get; private set; }
    public ILevelHandler LevelHandler { get; private set; }
    private float _lastUpdateTime;

    private PlayerData _playerData;
    private DataManager _dataManager;

    void Awake()
    {
        Instance = this;
        LevelHandler = new LevelHandler();
        LevelManager = LevelHandler.GetLevelManager();
        _dataManager = new DataManager();
        _dataManager.LoadAll();
        _playerData = _dataManager.Get<PlayerData>();
    }
    

	private void Update()
	{
		if (Time.time - _lastUpdateTime >= 1.0f)
        {
            _lastUpdateTime = Time.time;
            LevelManager.EverySecondUpdate();
        }
	}
}

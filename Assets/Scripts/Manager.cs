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

    public IPlayer Player { get; private set; }
    private DataManager _dataManager;

    void Awake()
    {
        Instance = this;
        LevelHandler = new LevelHandler();
        LevelManager = LevelHandler.GetLevelManager();
        _dataManager = new DataManager();
        _dataManager.LoadAll();
        Player = new Player(_dataManager);
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

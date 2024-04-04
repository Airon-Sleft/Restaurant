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
    public static LevelManager LevelManager { get; private set; }
    public LevelHandler LevelHandler { get; private set; }
    private float _lastUpdateTime;


    void Awake()
    {
        Instance = this;
        LevelHandler = new LevelHandler();
        LevelManager = LevelHandler.GetLevelManager();
    }

	private void Update()
	{
		if (Time.time - _lastUpdateTime >= 1.0f)
        {
            _lastUpdateTime = Time.time;
        }
	}
}

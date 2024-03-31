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
    public LevelHandler LevelHandler { get; private set; }
    private float _lastUpdateTime;


    void Awake()
    {
        Instance = this;
        LevelHandler = new LevelHandler();
    }

	private void Update()
	{
		if (Time.time - _lastUpdateTime >= 1.0f)
        {
            _lastUpdateTime = Time.time;
        }
	}

    public void OnWaiterGotKitchen(KitchenZone kitchen, Waiter waiter)
    {
        if (!waiter.IsHandFree()) Debug.Log("Free you hands at first");
        kitchen.AddFood(waiter.GetVisitor());
        //Resource res = kitchen.TakeIfPossible();
        //if (res == null) Debug.Log("There is no food");
        //waiter.AddResource(res);
        //Debug.Log("Go to visitor");
    }
    public void OnWaiterGotCashZone(IUnit waiter)
    {
        Debug.Log("Waiter got the CASH zone");
    }
}

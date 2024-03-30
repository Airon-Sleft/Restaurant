using Restaurant.Entity;
using Restaurant.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public Config config;
    public static Manager Instance { get; private set; }
    private LevelHandler _levelHandler;

    void Awake()
    {
        Instance = this;
        _levelHandler = new LevelHandler();
    }

    public void onVisitorGotTable(GameObject visitor)
    {
        Debug.Log("GOT TABLE");
    }

    public void onVisitorGotExit(GameObject visitor)
    {
        Debug.Log("DONE");
    }
    public void onPlayerGotTable(IVisitorSpace visitorSpace, IUnit waiter)
    {
        Debug.Log("Player got a TABLE");
        visitorSpace.SetState(VisitorSpace.TABLE_STATE.WAIT_FOR_ACTION);
    }
    public void onPlayerGotVisitor(IUnit visitor, IUnit waiter)
    {
        Debug.Log("Player got a visitor ");
    }
    public void onWaiterGotKitchen(IUnit waiter)
    {
        Debug.Log("Waiter got the kitchen");
    }

    public void onWaiterGotCashZone(IUnit waiter)
    {
        Debug.Log("Waiter got the CASH zone");
    }
}

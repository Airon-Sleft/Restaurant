using Restaurant.Entity;
using Restaurant.General;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject visitorPrefab;
    public GameObject tablePrefab;
    public GameObject exitObject;
    private GameObject player;
    private IObjectPooler objectPoolerVisitor;
    private IObjectPooler objectPoolerTable;
    public Config config;
    public static Manager Instance { get; private set; }

    private List<GameObject> visitorsList = new List<GameObject>();
    private List<GameObject> tableList = new List<GameObject>();


    void Awake()
    {
        Instance = this;
        objectPoolerVisitor = ObjectPoolFactory.Create(config.visitorPrefab, ObjectPoolFactory.POOLER_TYPE.EMPTY_ON_START);
        objectPoolerTable = ObjectPoolFactory.Create(config.tablePrefab, ObjectPoolFactory.POOLER_TYPE.EMPTY_ON_START);

        player = GameObject.Find("Player");
        //CreateVisitors(1);
        foreach (Vector3 pos in config.tablesPosition)
        {
            CreateTable(pos);
        }
    }


    private void CreateVisitors(int visitorCount)
    {
        for (int i = 0; i < visitorCount; i++)
        {
            GameObject visitor = objectPoolerVisitor.GetObject();
            visitor.transform.position = config.visitorSpawn[0];
            visitor.transform.rotation = visitorPrefab.transform.rotation;
            visitorsList.Add(visitor);
        }
    }

    private void DeleteVisitor(GameObject visitor)
    {
        visitorsList.Remove(visitor);
        Destroy(visitor);
    }

    private void CreateTable(Vector3 point)
    {
        GameObject table = objectPoolerTable.GetObject();
        table.transform.position = point;
        table.transform.rotation = tablePrefab.transform.rotation;
        tableList.Add(table);
    }

    public void onVisitorGotTable(GameObject visitor)
    {
        Debug.Log("GOT TABLE");
        visitor.GetComponent<EnemyController>().SetObjectToMove(exitObject);
    }

    public void onVisitorGotExit(GameObject visitor)
    {
        Debug.Log("DONE");
        DeleteVisitor(visitor);
    }
    public void onPlayerGotTable(IVisitorSpace visitorSpace, GameObject player)
    {
        Debug.Log("Player got a TABLE");
        visitorSpace.SetState(VisitorSpace.TABLE_STATE.WAIT_FOR_ACTION);
    }
    public void onPlayerGotVisitor(GameObject player)
    {
        Debug.Log("Player got a visitor");
    }
}

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

    private List<Vector3> tablePos = new List<Vector3> { 
        new Vector3 (-10, 0.22f, 1.8f),
        new Vector3 (-10, 0.22f, -5.5f),
        new Vector3 (10, 0.22f, -5.5f),
        new Vector3 (10, 0.22f, 1.8f),
    };

    private List<GameObject> visitorsList = new List<GameObject>();
    private List<GameObject> tableList = new List<GameObject>();


    private Vector3 visitorSpawnPos = new Vector3(0, 1, -6.5f);
    void Start()
    {
        objectPoolerVisitor = ObjectPoolFactory.Create(visitorPrefab, ObjectPoolFactory.POOLER_TYPE.EMPTY_ON_START);
        objectPoolerTable = ObjectPoolFactory.Create(tablePrefab, ObjectPoolFactory.POOLER_TYPE.EMPTY_ON_START);
        player = GameObject.Find("Player");
        CreateVisitors(1);
        foreach (Vector3 pos in tablePos)
        {
            CreateTable(pos);
        }
    }


    private void CreateVisitors(int visitorCount)
    {
        for (int i = 0; i < visitorCount; i++)
        {
            GameObject visitor = objectPoolerVisitor.GetObject();
            visitor.transform.position = visitorSpawnPos;
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
    public void onPlayerGotTable(GameObject player)
    {
        Debug.Log("Player got a TABLE");
    }
    public void onPlayerGotVisitor(GameObject player)
    {
        Debug.Log("Player got a visitor");
    }
}

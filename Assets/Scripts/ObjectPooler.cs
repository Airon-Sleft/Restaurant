using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : IObjectPooler
{
    private GameObject objectExample { get; set; }
    private Vector3 defaultPos;
    private Quaternion defaultRot;
    private List<GameObject> objectFreePool = new List<GameObject>();
    private List<GameObject> objectBusyPool = new List<GameObject>();
    private int smoothLimit = 20; // if high than limit, then when FreeObject we have to destroy object instead of pool it
    public ObjectPooler(GameObject objectReference)
    {
        objectExample = objectReference;
        defaultPos = objectReference.transform.position;
        defaultRot = objectReference.transform.rotation;
    }
    public GameObject GetObject()
    {
        foreach (GameObject obj in objectFreePool)
        {
            obj.SetActive(true);
            obj.transform.position = defaultPos;
            obj.transform.rotation = defaultRot;
            objectFreePool.Remove(obj);
            objectBusyPool.Add(obj);
            return obj;
        }
        GameObject objNew = GameObject.Instantiate(objectExample, defaultPos, defaultRot);
        objectBusyPool.Add(objNew);
        return objNew;
    }
    public void FreeObject(GameObject obj)
    {
        obj.SetActive(false);
        objectBusyPool.Remove(obj);
        if (objectBusyPool.Count + objectFreePool.Count > smoothLimit)
        {
            GameObject.Destroy(obj);
        }
        else
        {
            objectFreePool.Add(obj);
        }
    }

    public void ClearPool()
    {
        foreach (var obj in objectBusyPool)
        {
            GameObject.Destroy(obj);
        }
        foreach (var obj in objectFreePool)
        {
            GameObject.Destroy(obj);
        }
    }
}

public interface IObjectPooler
{
    public GameObject GetObject();
    public void ClearPool();
    public void FreeObject(GameObject obj);
}

public class ObjectPoolFactory
{
    public enum POOLER_TYPE
    {
        EMPTY_ON_START,
        FULL_ON_START,
    }
    public static IObjectPooler Create(GameObject objExample, POOLER_TYPE pType)
    {
        switch (pType) 
        {
            case POOLER_TYPE.EMPTY_ON_START:
                    return new ObjectPooler(objExample);
        }
        return null;
    }
}
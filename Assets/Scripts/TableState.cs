using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableState : MonoBehaviour
{
    public enum TABLE_STATE
    {
        BUSY,
        WAIT_FOR_ACTION,
        FREE_FOR_VISITOR,
    }
    public GameObject[] targetMeshPrefab;
    private List<GameObject> stateVisualObject;
    void Start()
    {
        stateVisualObject = new List<GameObject>(targetMeshPrefab.Length);
        for (int i = 0; i < targetMeshPrefab.Length; i++)
        {
            GameObject gameObject = Instantiate(targetMeshPrefab[i], transform.position, transform.rotation);
            gameObject.transform.Rotate(new Vector3(90, 0, 0));
            gameObject.transform.localScale = new Vector3(3.5f, 3.5f, 3.5f);
            gameObject.GetComponent<MeshCollider>().enabled = false;
            stateVisualObject.Add(gameObject);
        }
        setState(TABLE_STATE.FREE_FOR_VISITOR);
    }
    public void setState(TABLE_STATE newState)
    {
        turnOffPrefabs();
        if (newState == TABLE_STATE.WAIT_FOR_ACTION)
        {
            stateVisualObject[0].SetActive(true);
        }
        else if (newState == TABLE_STATE.FREE_FOR_VISITOR)
        {
            stateVisualObject[1].SetActive(true);
        }
    }

    private void turnOffPrefabs()
    {
        foreach (GameObject gameObject in stateVisualObject)
        {
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject objectPointToMove;
    private Rigidbody enemyRb;
    private GameObject manager;

    private float speed = 15.0f;
    void Start()
    {
        manager = GameObject.Find("Manager");
        enemyRb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (objectPointToMove != null)
        {
            enemyRb.AddForce((objectPointToMove.transform.position - transform.position).normalized * speed * Time.deltaTime, ForceMode.Impulse);
        }
    }

    public void SetObjectToMove(GameObject objectPoint)
    {
        objectPointToMove = objectPoint;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Table"))
        {
            manager.GetComponent<Manager>().onVisitorGotTable(this.gameObject);
        }

    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Exit"))
        {
            manager.GetComponent<Manager>().onVisitorGotExit(this.gameObject);
        }        
    }
}

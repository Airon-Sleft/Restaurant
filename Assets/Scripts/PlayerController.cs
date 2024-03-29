using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    private GameObject sceneManager;
    private Rigidbody _playerRB;
    private Animator playerAnimator;
    public float playerSpeed = 10.0f;
    private float _zBound = 9.0f;
    void Start()
    {
        _playerRB = GetComponent<Rigidbody>();
        sceneManager = GameObject.Find("Manager");
        playerAnimator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
        CheckBoundries();
    }

    private void MovePlayer()
    {
        float horValue = Input.GetAxis("Horizontal");
        float vertValue = Input.GetAxis("Vertical");
        Vector3 moveVector = new Vector3(horValue, 0.0f, vertValue);
        moveVector = Vector3.ClampMagnitude(moveVector, 1.0f);

        _playerRB.AddForce(moveVector * playerSpeed, ForceMode.Impulse);
        if (moveVector != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveVector);
            playerAnimator.SetFloat("Speed_f", 1f);
        }
        else
        {
            playerAnimator.SetFloat("Speed_f", 0f);

        }
    }
    private void CheckBoundries()
    {
        if (Mathf.Abs(transform.position.z) > _zBound)
        {
            float newVel = Mathf.Max(Mathf.Abs(_playerRB.velocity.z), 24) * (transform.position.z > 0 ? -1 : 1);
            _playerRB.AddForce(new Vector3(0, 0, newVel), ForceMode.VelocityChange);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Table"))
        {
            sceneManager.GetComponent<Manager>().onPlayerGotTable(gameObject);
        }
        else if (collision.gameObject.CompareTag("Visitor"))
        {
            sceneManager.GetComponent<Manager>().onPlayerGotVisitor(gameObject);
        }
    }
}

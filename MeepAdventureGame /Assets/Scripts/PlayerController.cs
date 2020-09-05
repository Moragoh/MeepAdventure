using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 change;
    private RigidBody2D myRigidbody;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() {

    }

    void HandleMovement() {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");

        if (change != Vector3.zero) {
            change.Normalize();
            myRigidbody.MovePosition(transform.position + change * moveSpeed * Time.deltaTime);
        }
    }
}

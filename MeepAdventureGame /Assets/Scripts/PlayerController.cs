using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool onGround;
    [SerializeField] Transform onGroundChecker;
    [SerializeField] float groundCheckRadius;
    [SerializeField] public LayerMask groundLayer;

    private Vector3 change;
    private Rigidbody2D myRigidbody;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        //anim = GetComponent<Animator>();
		myRigidbody = GetComponent<Rigidbody2D>();
        onGround = true;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        CheckIfOnGround();
    }

    void FixedUpdate() {

    }

    void CheckIfOnGround()
    {
        Collider2D collider = Physics2D.OverlapCircle(onGroundChecker.position, groundCheckRadius, groundLayer);

        if (collider != null)
        {
            onGround = true;
        }
        else if (collider == null)
        {
            onGround = false;
        }
    }

    void HandleMovement() {
        change.x = Input.GetAxisRaw("Horizontal");
        float moveBy = change.x * moveSpeed;

        if (change != Vector3.zero) {
            //change.Normalize();
            //myRigidbody.MovePosition(transform.position + change * moveSpeed * Time.deltaTime);
            myRigidbody.velocity = new Vector2(moveBy, myRigidbody.velocity.y);
        }

        if (Input.GetButtonDown("Jump") == true && onGround)
        {
            print("jumped");
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
        }
    }
}

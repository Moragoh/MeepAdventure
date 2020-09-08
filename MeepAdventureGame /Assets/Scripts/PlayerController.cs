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
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private GameObject deathAnim;

    private Vector3 change;
    private Rigidbody2D myRigidbody;
    private Animator anim;
    private bool movingRight = true;
    private bool canMove = true;
    private bool isDying = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "death")
        {
            canMove = false;

            if (isDying == false)
            {
                StartCoroutine(PlayDeathAndRespawnCo());
            }
          
        }
    }

    // Coroutine for death animation and respawn
    private IEnumerator PlayDeathAndRespawnCo()
    {
        isDying = true;

        // Turns player invisible
        gameObject.GetComponent<SpriteRenderer>().enabled = false;

        // Instantiate
        GameObject effect = Instantiate(deathAnim, transform.position, Quaternion.identity);
        Destroy(effect, 1f);

        // Wait seconds
        yield return new WaitForSeconds(1.2f);

        // Teleport to spawn
        this.transform.position = spawnPoint.transform.position;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;

        isDying = false;
        canMove = true;
    }

    void CheckIfOnGround()
    {
        Collider2D collider = Physics2D.OverlapCircle(onGroundChecker.position, groundCheckRadius, groundLayer);

        if (collider != null)
        {
            onGround = true;
            anim.SetBool("isJumping", false);
        }
        else if (collider == null)
        {
            onGround = false;
            anim.SetBool("isJumping", true);
        }
    }

    void HandleMovement() {
        change.x = Input.GetAxisRaw("Horizontal");
        float moveBy = change.x * moveSpeed;

        if (change != Vector3.zero && canMove) {
            //change.Normalize();
            //myRigidbody.MovePosition(transform.position + change * moveSpeed * Time.deltaTime);
            anim.SetBool("isMoving", true);
            myRigidbody.velocity = new Vector2(moveBy, myRigidbody.velocity.y);
        }
        else
        {
            anim.SetBool("isMoving", false);
        }

        if (Input.GetButtonDown("Jump") == true && onGround && canMove)
        {
            // Perform jump
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpForce);
        }

        // Animate player walking to the right or left
        if (change.x > 0 && !movingRight)
        {
            FlipPlayer();
        }
        else if (change.x < 0 && movingRight)
        {
            FlipPlayer();
        }
    }

    void FlipPlayer()
    {
        movingRight = !movingRight;

        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

}

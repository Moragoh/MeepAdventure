using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OctopiBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    private int direction = 1;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveOctopus();
    }

    void MoveOctopus()
    {
        float moveBy = direction * moveSpeed;

        rb.velocity = new Vector2(moveBy, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "return")
        {
            direction *= -1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextboxManager : MonoBehaviour
{
    [SerializeField] private GameObject message;

    private Animator anim;
    private bool messageOpen = false;
    private bool nearMessage;

    // Start is called before the first frame update
    void Start()
    {
        anim = message.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckForMessageOpen();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "message")
        {
            nearMessage = true;
            print("bruh");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       if (other.tag == "message")
        {
            nearMessage = false;
        }
    }

    private void CheckForMessageOpen()
    {
        if (nearMessage && Input.GetButtonDown("Interact"))
        {
            print("bruh2");
            if (!messageOpen)
            {
                // Rollup message
                message.SetActive(true);
                anim.SetTrigger("rollUp");
                messageOpen = !messageOpen;
            }
            else
            {
                anim.SetTrigger("rollDown");
                message.SetActive(false);
                messageOpen = !messageOpen;
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float thrust;

    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        //Sets the rigidbody
        rb = GetComponent<Rigidbody2D>();

        //Sets the animator to the first childs animator
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //Calls the Move() function
        Move();
    }

    public void Move()
    {
        //Gets the horizontal input (-1 to 1)
        float horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            //Sets the velocity of the player
            rb.velocity = new Vector2(Vector2.right.x * horizontal * speed * Time.deltaTime, rb.velocity.y);

            //Rotates the player
            transform.localScale = new Vector3(horizontal, 1, 1);

            //Sets isWalking to true, swithing to walk animation
            anim.SetBool("isWalking", true);
        }
        else
        {
            //Set the velocity to zero, and keeps the vertical speed/velocity
            rb.velocity = new Vector2(0, rb.velocity.y);

            //Sets isWalking to false, swithing to idle animation
            anim.SetBool("isWalking", false);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //Checks if the object it collides with has the tag "JumpBoost"
        if (other.gameObject.CompareTag("JumpBoost"))
        {
            //Adds an upwards force
            rb.AddForce(Vector2.up * thrust, ForceMode2D.Impulse);

            //Destroys the object the player collides with that has the tag
            Destroy(other.gameObject);
        }
    }
}

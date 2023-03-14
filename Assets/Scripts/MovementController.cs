using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float movementSpeed = 6.0f;
    public float jumpSpeed = 1.2f;

    private bool jumping = false;
    private bool isGrounded = true;

    Rigidbody2D rb2D;
    Animator animator;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        addAnimations();
        jump();
        move();
    }

    private void addAnimations()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("walkRight", true);
        }
        else
        {
            animator.SetBool("walkRight", false);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("walkLeft", true);
        }
        else
        {
            animator.SetBool("walkLeft", false);
        }
    }

    private void move()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * movementSpeed;
    }

    private void jump()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetKey(KeyCode.Space)) && !jumping && isGrounded)
        {
            rb2D.AddForce(new Vector2(0f, jumpSpeed), ForceMode2D.Impulse);
            Time.timeScale = 1;
            rb2D.velocity = Vector2.up * jumpSpeed;
            jumping = true;
            isGrounded = false;
            StartCoroutine(DisableJumping());
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "ground") {
            isGrounded = true;
        }
    }

    private IEnumerator DisableJumping()
    {
        yield return new WaitForSeconds(0.2f);
        jumping = false;
    }
}

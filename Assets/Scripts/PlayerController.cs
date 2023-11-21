using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D rb;

    [SerializeField] private float speed = 0;
    [SerializeField] private float minSpeed = 0.5f;
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float acceleration = 0.5f;
    [SerializeField] private float jumpSpeed;

    private bool isGrounded;
    private Vector2 movement;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {   
        if (movement.x <= 0)
        {   
            if (speed >= minSpeed)
            {
                speed -= acceleration * Time.deltaTime;
            }
        }
        if (movement.x >= 0)
        {
            if (speed <= maxSpeed) 
            {
                
            }
        }

        if (rb.velocity.y <= 0)


        rb.velocity = new Vector2 (movement.x * speed, rb.velocity.y);
    }


    private void OnMovement(InputValue input)
    {
        movement = input.Get<Vector2>();
    }

    private void OnJump(InputValue input)
    {
        if (isGrounded)
        {
            rb.velocity = new Vector2 (movement.x, jumpSpeed);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Collectable")
        {
            Destroy(collision.gameObject);
        }
    }

}

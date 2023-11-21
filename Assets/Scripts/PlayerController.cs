using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Credit:
    //https://www.youtube.com/watch?v=KbtcEVCM7bw

    private Rigidbody2D rb;

    [SerializeField] private float speed = 1;
    [SerializeField] private float minSpeed = 1;
    [SerializeField] private float maxSpeed = 5;
    [SerializeField] private float acceleration = 0.8f;
    [SerializeField] private float jumpSpeed = 8;

    private bool isGrounded;
    private Vector2 movement;
    


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        rb.velocity = new Vector2(movement.x * speed, rb.velocity.y);

        if (movement.x == 0)
        {   
            if (speed >= minSpeed)
            {
                speed -= acceleration * Time.deltaTime;
            }
        }
        else
        {
            if (speed <= maxSpeed) 
            {
                speed += acceleration * Time.deltaTime;
            }
        }
        
        
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

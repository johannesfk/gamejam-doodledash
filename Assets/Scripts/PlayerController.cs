using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Vector2 movement;

    [Header("GroundedMovement")]
    [SerializeField] private float speed = 8;
    [SerializeField] private float speedPower = 1f;
    [SerializeField] private float acceleration = 13f;
    [SerializeField] private float decceleration = 16f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 15;
    [SerializeField] private float fallGravity = 2;
    [SerializeField] private float jumpBuffer = 0.2f;
    [SerializeField] private float jumpCutMultiplier = 0.5f;
    private float jumpBufferTimer;
    private float gravityScale = 1;
    
    private bool jumpCutted = false;

    private bool isGrounded;
    private bool isJumping;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        gravityScale = rb.gravityScale;
        Debug.Log(gravityScale);

        isJumping = false;

    }

    private void FixedUpdate()
    {

        #region This Code was made with this tutorial: https://www.youtube.com/watch?v=KbtcEVCM7bw
        //Calculating next direction of movement
        float nextMove = movement.x * speed;

        //Calculating the speed we are going and speed we want to be
        float speedDif = nextMove - rb.velocity.x;

        //calculating our accelarition/decceleration rate
            //Mathf.Abs returns an absolute meaning it cant be negative
            //not sure about the "?"
        float accelerationRate = (Mathf.Abs(nextMove) > 0) ? acceleration : decceleration;

        //Calculation The characters movement itself
            // Mathf.Sign returns a value of 1 or -1 to indicate the direction being right or left
            // Mathf.Pow Means to the power of something this is needed since its an acceleration and we want it to be smooth
        float moveAction = Mathf.Pow(Mathf.Abs(speedDif) * accelerationRate, speedPower) * Mathf.Sign(speedDif);

        rb.AddForce(moveAction * Vector2.right);
        #endregion

        if (rb.velocity.y < 0)
        {
            rb.gravityScale = gravityScale * fallGravity;
        }
        else
        {
            rb.gravityScale = gravityScale;
        }

            if (jumpBufferTimer > 0)
            {   
                 jumpBufferTimer -= Time.fixedDeltaTime;
                 if (isGrounded)
                 {
                    rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                    Debug.Log("JUMP BUFFERED");
                    isJumping = true;
                    isGrounded = false;
                    
                 }
            }

    }

    private void OnMovement(InputValue input)
    {
        movement = input.Get<Vector2>();
    }

    private void OnJump(InputValue buttonPress)
    {   
        //Jumping
        if (isGrounded)
        {
            isJumping = true;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            isGrounded = false;
        }
        else if (isGrounded == false)
        {   
            jumpBufferTimer = jumpBuffer;
        }

        //if (buttonPress.canceled)
        //{
            //Debug.Log("JumpCutShort");
            //jumpCutted = true;
            //rb.AddForce(Vector2.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
        //}

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
            isJumping = false;
            jumpCutted = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
            jumpCutted = false;
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

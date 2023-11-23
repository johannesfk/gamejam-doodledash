using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public ParticleSystem dust;
    private Color platformColor;

    private GameActions playerControls;
    private InputAction jumpAction;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;

    public CardStack cardStack;

    [Header("GroundedMovement")]
    [SerializeField] private float speed = 15;
    [SerializeField] private float speedPower = 0.9f;
    [SerializeField] private float acceleration = 4f;
    [SerializeField] private float decceleration = 12f;

    [Header("Jump Settings")]
    [SerializeField] private float jumpForce = 12;
    [SerializeField] private float fallGravity = 4;
    [SerializeField] private float jumpBuffer = 0.1f;
    [SerializeField] private float jumpCutMultiplier = 0.5f;
    [SerializeField] private float jumpCutWindow = 1f;
    [SerializeField] private float dJMultiplier = 2;
    [SerializeField] private float coyoteTimeWindow = 0.05f;
    private float jumpBufferTimer;
    private float coyoteTimeTimer;
    private float gravityScale = 1;
    private float jumpCutTimer;
    private bool jumpCutted = false;
    private bool isGrounded;
    private bool isJumping;
    private bool canBuffer;

    [Header("PowerUp Settings")]
    [SerializeField] private float wallJumpXmultiplier = 15;
    [SerializeField] private float dashSpeed = 100;
    [SerializeField] private float dashAngleMultiplier = 0.3f;
    [SerializeField] private float bounceMultiplier = 1.2f;
    [SerializeField] private GameObject tpPosition;
    [SerializeField] private float tpPositionMultiplier = 5;
    private bool touchingWall = false;
    private bool bounceActivated = false;
    private float bouncePower;
    private PowerType nextPower;
    private Vector3 wallPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerControls = new GameActions();
        jumpAction = playerControls.Movement.Jump;

    }
    private bool hasWon = false;
    private bool levelCompleted = false;


    private void OnEnable()
    {
        playerControls.Enable();
        jumpAction.canceled += Jump;
    }
    private void OnDisable()
    {
        playerControls.Disable();
        jumpAction.canceled -= Jump;
    }

    private void Start()
    {
        gravityScale = rb.gravityScale;
        isJumping = false;



    }



    private void FixedUpdate()
    {

        /* animator.SetFloat("VelocityX", movement.x);
        animator.SetFloat("VelocityY", rb.velocity.y); */

        var ps = dust.main;
        ps.startColor = platformColor;

        #region This Code was made with this tutorial: https://www.youtube.com/watch?v=KbtcEVCM7bw
        //Calculating next direction of movement
        float nextMove = movement.x * speed;

        //Calculating the speed we are going and speed we want to be
        float speedDif = nextMove - rb.velocity.x;

        /// Calculating our accelarition/decceleration rate
        /// Mathf.Abs returns an absolute meaning it cant be negative
        /// not sure about the "?"
        /// That one is a ternary operator it works like this
        /// (condition) ? (if true) : (if false)
        float accelerationRate = (Mathf.Abs(nextMove) > 0) ? acceleration : decceleration;

        //Calculation The characters movement itself
        // Mathf.Sign returns a value of 1 or -1 to indicate the direction being right or left
        // Mathf.Pow Means to the power of something this is needed since its an acceleration and we want it to be smooth
        float moveAction = Mathf.Pow(Mathf.Abs(speedDif) * accelerationRate, speedPower) * Mathf.Sign(speedDif);

        rb.AddForce(moveAction * Vector2.right);
        // print(rb.velocity.x);

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

            if (canBuffer)
            {
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                Debug.Log("JUMP BUFFERED");
                isJumping = true;
                canBuffer = false;
            }
        }


        if (jumpCutTimer > 0)
        {
            jumpCutTimer -= Time.fixedDeltaTime;
        }

        if (coyoteTimeTimer > 0)
        {
            coyoteTimeTimer -= Time.fixedDeltaTime;
        }

        //PowerUps
        if (cardStack.cards.Count > 0)
        {
            nextPower = cardStack.cards[0].power;
        }

        if (bounceActivated)
        {
            if (!isGrounded)
            {
                bouncePower = Mathf.Abs(rb.velocity.y);
            }

            if (isGrounded)
            {
                rb.AddForce(Vector2.up * bouncePower * bounceMultiplier, ForceMode2D.Impulse);
                FindObjectOfType<AudioManager>().Play("Bounce");
                bounceActivated = false;
                // Debug.Log(bouncePower);
            }


        }

        if (movement.x != 0)
        {
            tpPosition.transform.position = rb.position + movement * tpPositionMultiplier;
        }


        if (hasWon == true)
        {
            // Debug.Log("Du har vundet!");
            GameManager gameManager = FindObjectOfType<GameManager>();
            if (!levelCompleted)
            {
                gameManager.LevelComplete();
                levelCompleted = true;
            }
        }
    }


    private void OnMovement(InputValue input)
    {
        movement = input.Get<Vector2>();


        if (isGrounded)
        {
            CreateDust();
        }

    }

    private void OnJump(InputValue buttonPress)
    {
        //Jumping
        if (isGrounded)
        {
            if (!isJumping)
            {
                jumpCutTimer = jumpCutWindow;
                isJumping = true;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                FindObjectOfType<AudioManager>().Play("Jump");
                CreateDust();
                isGrounded = false;
            }
        }
        else if (isGrounded == false)
        {
            if (coyoteTimeTimer > 0)
            {
                Debug.Log("COYOTE JUMP MOVE");
                jumpCutTimer = jumpCutWindow;
                isJumping = true;
                rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                FindObjectOfType<AudioManager>().Play("Jump");
            }

            jumpBufferTimer = jumpBuffer;

        }

    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (jumpCutted == false)
        {
            if (rb.velocity.y > 0 && jumpCutTimer > 0)
            {
                jumpCutted = true;
                Debug.Log("JumpCutted");
                rb.AddForce(Vector2.down * rb.velocity.y * (1 - jumpCutMultiplier), ForceMode2D.Impulse);
            }
        }
    }

    private void OnUsePowerUp()
    {
        if (cardStack.cards.Count > 0)
        {
            switch (nextPower)
            {
                case PowerType.Dash:

                    if (movement.x != 0)
                    {
                        Debug.Log("DASH MOVE");

                        rb.AddForce(Vector2.right * movement * dashSpeed + Vector2.up * dashAngleMultiplier, ForceMode2D.Impulse);
                        FindObjectOfType<AudioManager>().Play("Dash");
                        cardStack.Use();
                    }


                    break;

                case PowerType.DoubleJump:
                    if (!isGrounded)
                    {
                        Debug.Log("DOUBLE JUMP MOVE");
                        FindObjectOfType<AudioManager>().Play("Jump");
                        if (rb.velocity.y < 0)
                        {

                            rb.AddForce(Vector2.up * jumpForce * dJMultiplier, ForceMode2D.Impulse);


                        }
                        else
                        {
                            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                        }
                        cardStack.Use();
                    }
                    break;

                case PowerType.WallJump:

                    if (touchingWall)
                    {
                        Debug.Log("WALL JUMP MOVE");

                        float wallJumpXDirection = Mathf.Sign(gameObject.transform.position.x - wallPos.x);

                        rb.AddForce(Vector2.up * jumpForce + Vector2.right * wallJumpXDirection * wallJumpXmultiplier, ForceMode2D.Impulse);
                        FindObjectOfType<AudioManager>().Play("Jump");
                        cardStack.Use();
                    }

                    break;

                case PowerType.Teleport:
                    Debug.Log("TP MOVE");

                    transform.position = tpPosition.transform.position;
                    FindObjectOfType<AudioManager>().Play("Teleport");
                    cardStack.Use();


                    break;

                case PowerType.Bounce:

                    if (!isGrounded)
                    {
                        Debug.Log("BOUNCE MOVE");

                        bounceActivated = true;
                        cardStack.Use();
                    }

                    break;
                default:
                    break;
            }

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            canBuffer = true;
            isGrounded = true;
            isJumping = false;
            jumpCutted = false;
            platformColor = collision.gameObject.GetComponent<SpriteRenderer>().color;
            CreateDust();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }

        if (collision.gameObject.tag == "Wall")
        {
            touchingWall = true;

            wallPos = collision.transform.position;


            Debug.Log("Væg Moment");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            jumpCutted = false;
            canBuffer = false;

            if (isJumping == false)
            {
                coyoteTimeTimer = coyoteTimeWindow;
            }

        }

        if (collision.gameObject.tag == "Wall")
        {
            touchingWall = false;
        }

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Collectable"))
        {
            Destroy(collision.gameObject);
        }
        /*  if (collision.gameObject.CompareTag("Exit"))
         {
             if (Collectables.allCollected)
             {
                 Debug.Log("Du har vundet!");
             }
         } */
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Exit"))
        {
            Debug.Log("Du ramte exit");
            // Debug.Log(Collectables.allCollected);
            if (Collectables.allCollected)
            {
                hasWon = true; // To prevent multiple win states - Debounce
            }
        }
    }

    void CreateDust()
    {
        dust.Play();
    }

}

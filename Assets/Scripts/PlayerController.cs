using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Player Setting")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float movementForceInAir;
    [SerializeField] private float jumpForce;
    [SerializeField] private float maxJumps;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private float wallCheckDistance;
    [SerializeField] private float wallSlidingSpeed;
    [SerializeField] private float airDragMultiplier;
    [SerializeField] private float variableJumpMultiplier = 0.5f;

    private float amountOfJumpLeft;

    private bool isFacingRight = true;
    private bool isGround;
    private bool canJump;
    private bool isTouchingWall;
    private bool isTouchingLedge;
    private bool isWallSliding;
    private bool canClimbLedge;
    

    private Vector2 moveInput;
    private Vector2 ledgePosBot;
    private Vector2 ledgePos1;
    private Vector2 ledgePos2;

    public Transform groundCheck;
    public Transform wallCheck;

    [SerializeField] private LayerMask WhatIsGround;

    private Rigidbody2D rb;
    private PlayerInputAction playerInputAction;
    private PlayerAttack playerAttack;
    // Start is called before the first frame update
    void Start()
    {
        amountOfJumpLeft = maxJumps;
        groundCheck = GameObject.Find("GroundCheck").transform;
        wallCheck = GameObject.Find("WallCheck").transform;
        if(groundCheck == null)
        {
            groundCheck.position = transform.position;
        }
        rb = GetComponent<Rigidbody2D>();
        playerInputAction = new PlayerInputAction();
        playerAttack = GetComponent<PlayerAttack>();
        playerInputAction.MoveOnGround.Enable();
        playerInputAction.MoveOnGround.Jump.performed += Jump_performed;
        playerInputAction.MoveOnGround.Jump.canceled += Jump_canceled;
        playerInputAction.MoveOnGround.Attack.performed += Attack_performed;
    }

    private void Attack_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        playerAttack.Attack();
    }

    private void Jump_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (!isTouchingWall)
        {
            rb.velocity = new Vector2(rb.velocity.x, variableJumpMultiplier * rb.velocity.y);
        }
        else {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
    }

    private void Jump_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            amountOfJumpLeft--;
        }
         
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        CheckMovementDirection();
        CheckSurroundings();
        CheckIfCanJump();
        CheckIsWallSliding();
        //CheckLedgeClimb();
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        UpdateAnimations();
    }
   

   
    private void CheckIsWallSliding()
    {
        if(isTouchingWall && rb.velocity.y < 0 && !canClimbLedge)
        {
            isWallSliding = true;
        }
        else isWallSliding = false;
    }
    private void CheckInput()
    {
        moveInput = playerInputAction.MoveOnGround.Movement.ReadValue<Vector2>();
    }

    private void CheckMovementDirection()
    {
        if (isFacingRight && moveInput.x < 0)
        {
            Flip();
        }
        else if(!isFacingRight && moveInput.x > 0)
        {
            Flip(); 
        }
    }

    private void CheckIfCanJump()
    {
        if (isGround && rb.velocity.y <= 0.0001f)
        {
            amountOfJumpLeft = maxJumps;
        }

        if(amountOfJumpLeft <= 0) canJump = false;
        else canJump = true;
    }

    private void CheckSurroundings()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position,
            transform.right, wallCheckDistance, WhatIsGround);
        
    }


    private void Flip()
    {
        if (!isWallSliding)
        {
            isFacingRight = !isFacingRight;
            transform.Rotate(0f, 180f, 0f);
        }
    }
    private void ApplyMovement()
    {
        if(!isGround && !isWallSliding && moveInput.x == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(moveInput.x * movementSpeed, rb.velocity.y);
        }
        

        if (isWallSliding)
        {
            if(rb.velocity.y < wallSlidingSpeed) 
                rb.velocity = new Vector2(rb.velocity.x, -wallSlidingSpeed);
        }

    }

    private void UpdateAnimations()
    {
        PlayerAnim.instance.SetXVelocity(Mathf.Abs(rb.velocity.x));
        PlayerAnim.instance.SetIsGrounded(isGround);
        PlayerAnim.instance.SetYVelocity(rb.velocity.y);
        PlayerAnim.instance.SetIsWallSliding(isWallSliding);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance,
                         wallCheck.position.y, wallCheck.position.z));
    }
}

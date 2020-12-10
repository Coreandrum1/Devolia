using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Vector2 colliderStandOffset;
    public Rigidbody2D rigidBody;
    BoxCollider2D bodyCollider;
    private float moveInput; 

    private Animator animator;

    public float speed = 16f;
    public float maxFallSpeed = -1f;

    int direction = 1;
    float originalXScale;

    public float rayOffset = .4f;
    public float groundDistance = .2f;
    public float jumpForce = 30f;


    public bool isJumping;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    public bool isOnGround;





    // Start is called before the first frame update
    void Start()
    {
        originalXScale = transform.localScale.x;
        animator = GetComponent<Animator>();
    }

    // useful variables
    public float horizontal;




    // Update is called once per frame
    void Update()
    {
        isJumping = false;

        isOnGround = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
        animator.SetBool("isGrounded", isOnGround);

        if (isOnGround == true && Input.GetKeyDown(KeyCode.W))
        {
            rigidBody.velocity = Vector2.up * jumpForce;
        }

        ProcessInputs();
        

    }

    private void FixedUpdate() {
        Movement();
        horizontal = Mathf.Clamp(horizontal, -1f, 1f);
    }

    void ProcessInputs()
    {
        horizontal = Input.GetAxis("Horizontal");
    }

    void Movement()
    {
        float xVelocity = speed * horizontal;

        if (xVelocity * direction < 0f)
            FlipCharacterDirection();

        rigidBody.velocity = new Vector2(xVelocity, rigidBody.velocity.y);
        animator.SetFloat("velocityX", Mathf.Abs(xVelocity));
    }

    void Jumping()
    {

    }

    void FlipCharacterDirection()
    {
		//Turn the character by flipping the direction
		direction *= -1;

		//Record the current scale
		Vector3 scale = transform.localScale;

		//Set the X scale to be the original times the direction
		scale.x = originalXScale * direction;

		//Apply the new scale
		transform.localScale = scale;
    }
}

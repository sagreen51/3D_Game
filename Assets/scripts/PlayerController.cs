using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
    public float moveSpeed;
    public float runSpeed;

    public Rigidbody rb;
    public float jumpVelocity;
    public Collider col;
    public Material green;
    public Material pink;

    [Range(0f, 1f)]
    public float horizontalDamping;
    [Range(0f,1f)]
    public float verticalDamping;
    [Range(0f, 1f)]
    public float horizontalJumpDamping;
    [Range(0f, 1f)]
    public float verticalJumpDamping;

    [Range(0f, 1f)]
    public float distToGroundPadding;
    
    [SerializeField]
    private float jumpPressedRememberTime;
    [SerializeField]
    private float groundedRememberTime;
    [SerializeField]
    private float deltaTimeMultiplier;

    private float jumpPressedRemember;
    private float groundedRemember;
    private float horizontal;
    private float vertical;
    private float distToGround;
    private float baseMoveSpeed;
    private MeshRenderer mesh;
    private Material red;
    private Vector3 velocity;
    private Vector3 acc;
    private float runVel;
    [HideInInspector]
    public bool isOnWall;
    
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        distToGround = col.bounds.extents.y;
        baseMoveSpeed = moveSpeed;
        mesh = GetComponent<MeshRenderer>();
        red = mesh.material;
        acc = Physics.gravity = new Vector3(0, -9.8f, 0);
	}
	
	// Update is called once per frame
	void Update () {

        movement();
        commands();

    }

    private void movement()
    {
        if (runVel > 0.19f || Input.GetButton("Run")) //deadzone
        {
            moveSpeed = runSpeed;
        }
        else
        {
            moveSpeed = baseMoveSpeed;
        }
        
        if (!isGrounded())  //if airborne
        {
            if(rb.velocity.y < (jumpVelocity/1.15))
            {
                if (groundedRemember != groundedRememberTime)
                {
                    Physics.gravity = acc * 4f;
                }
                else
                {
                    Physics.gravity = acc;
                }
            }
            
            horizontal = rb.velocity.x;
            horizontal += Input.GetAxisRaw("Horizontal");
            horizontal *= Mathf.Pow(1f - horizontalJumpDamping, Time.deltaTime * deltaTimeMultiplier);

            vertical = rb.velocity.z;
            vertical += Input.GetAxisRaw("Vertical");
            vertical *= Mathf.Pow(1f - verticalJumpDamping, Time.deltaTime * deltaTimeMultiplier);
        }
        else 
        {
            if(moveSpeed != runSpeed && moveSpeed != baseMoveSpeed)
            {
                moveSpeed = baseMoveSpeed;
            }
            Physics.gravity = acc;

            horizontal = rb.velocity.x;
            horizontal += Input.GetAxisRaw("Horizontal");
            horizontal *= Mathf.Pow(1f - horizontalDamping, Time.deltaTime * deltaTimeMultiplier);

            vertical = rb.velocity.z;
            vertical += Input.GetAxisRaw("Vertical");
            vertical *= Mathf.Pow(1f - verticalDamping, Time.deltaTime * deltaTimeMultiplier);

        }
        
        groundedRemember -= Time.deltaTime;
        if (isGrounded())
        {
            groundedRemember = groundedRememberTime;
        }

        jumpPressedRemember -= Time.deltaTime;
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressedRemember = jumpPressedRememberTime;
        }

        if ((jumpPressedRemember > 0) && ((groundedRemember > 0) || isOnWall))
        {
            jumpPressedRemember = 0;
            groundedRemember = 0;
            rb.velocity = new Vector3(rb.velocity.x, jumpVelocity, rb.velocity.z);
        }
        else
        {
            rb.velocity = new Vector3(horizontal * moveSpeed, rb.velocity.y, vertical * moveSpeed);
        }
        
    }

    private void commands()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            mesh.material = pink;
        }
        else if(Input.GetButtonDown("Fire2"))
        {
            mesh.material = green;

        }
        else if(Input.GetButtonDown("Fire3"))
        {
            mesh.material = red;
        }

        runVel = -Input.GetAxis("Run");
    }

    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + distToGroundPadding);
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.collider.tag == "Wall")
        {
            isOnWall = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isOnWall = false;
    }
}

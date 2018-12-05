using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
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
    
    [Range(0f, 2f)]
    public float airborneSpeed;
    
    [Range(1f, 1.1f)]
    public float horizontalJumpScale;
    [Range(1f, 1.1f)]
    public float verticalJumpScale;

    [SerializeField]
    private float jumpPressedRememberTime;
    [SerializeField]
    private float groundedRememberTime;
    
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
        if (!isGrounded())  //if airborne
        {
            if(rb.velocity.y < (jumpVelocity / 1.5f)) {
                Physics.gravity = acc * 4f;
            }

            moveSpeed = airborneSpeed;
            rb.velocity = new Vector3(rb.velocity.x * horizontalJumpScale, rb.velocity.y, rb.velocity.z * verticalJumpScale);

            horizontal = rb.velocity.x;
            horizontal += Input.GetAxisRaw("Horizontal");
            horizontal *= Mathf.Pow(1f - horizontalJumpDamping, Time.deltaTime * 10f);

            vertical = rb.velocity.z;
            vertical += Input.GetAxisRaw("Vertical");
            vertical *= Mathf.Pow(1f - verticalJumpDamping, Time.deltaTime * 10f);
        }
        else 
        {
            if(moveSpeed != baseMoveSpeed)
            {
                moveSpeed = baseMoveSpeed;
            }
            Physics.gravity = acc;

            horizontal = rb.velocity.x;
            horizontal += Input.GetAxisRaw("Horizontal");
            horizontal *= Mathf.Pow(1f - horizontalDamping, Time.deltaTime * 10f);

            vertical = rb.velocity.z;
            vertical += Input.GetAxisRaw("Vertical");
            vertical *= Mathf.Pow(1f - verticalDamping, Time.deltaTime * 10f);

        }
        
        rb.velocity = new Vector3(horizontal * moveSpeed, rb.velocity.y, vertical * moveSpeed);

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

        if ((jumpPressedRemember > 0) && (groundedRemember > 0))
        {
            jumpPressedRemember = 0;
            groundedRemember = 0;
            rb.velocity = new Vector3(rb.velocity.x * horizontalJumpScale, jumpVelocity, rb.velocity.z * verticalJumpScale);
        }

        //Debug.Log(Physics.gravity);
        //Debug.Log(rb.velocity);
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
    }

    public bool isGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + distToGroundPadding);
    }
    
}

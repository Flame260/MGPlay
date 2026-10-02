using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 6f;
    public float jumpForce = 5f;
    public float turnSpeed = 15f;
    public float fallLmit = -15f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    Rigidbody rb;
    Vector3 input;
    bool Jump;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.timeScale == 0)
        {
            return;
        }
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        
        input = new Vector3(horizontal, 0, vertical).normalized;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump = true;
        }

        if (transform.position.y < fallLmit)
        {
            GameManager2.Instance.GameOver();
        }

    }

    void FixedUpdate()
    {
        Vector3 move = input* moveSpeed ;
        rb.linearVelocity = new Vector3(move.x, rb.linearVelocity.y, move.z);

        if (input != Vector3.zero)
        {
            Quaternion look = Quaternion.LookRotation(input);
            transform.rotation = Quaternion.Slerp(rb.rotation,look, turnSpeed * Time.fixedDeltaTime);
        }

        if(Jump && IsGrounded())
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
            Jump = false;
        }
        Jump = false;

    }
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f);
    }
}

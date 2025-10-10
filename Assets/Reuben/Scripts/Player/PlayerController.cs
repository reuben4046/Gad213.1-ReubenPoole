using System.Collections;
using System.Drawing;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private LedgeDetection topLedgeCollider;
    [SerializeField] private LedgeDetection bottomLedgeCollider;

    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float maxMoveSpeed = 5f;
    [SerializeField] private float acceleration = 30f;

    [SerializeField] private float jumpForce = 10f;

    [SerializeField] private float coyoteTime = 0.2f;
    private float coyoteTimer;

    [SerializeField] private float airGravity;
    [SerializeField] private float fallGravity;

    [SerializeField] private float jumpBufferLength = 0.1f;
    private float jumpBufferCount;

    [Range(0f, .1f)]
    [SerializeField] private float ledgeHopSize;

    private float horizontalInput;

    [HideInInspector]
    public bool isUsingPushPull;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine(GravityController());
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Coyote time
        if (IsGrounded()) coyoteTimer = coyoteTime;
        else coyoteTimer -= Time.deltaTime;

        // jump buffer
        if (Input.GetKeyDown(KeyCode.Space)) jumpBufferCount = jumpBufferLength;
        else jumpBufferCount -= Time.deltaTime;

        // jump
        if (jumpBufferCount >= 0 && coyoteTimer > 0f && rb.linearVelocity.y < 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
            jumpBufferCount = 0;
        }

        // variable jump height
        if (Input.GetKeyUp(KeyCode.Space) && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }

        //ledge hop
        DetectLedges();
    }

    void FixedUpdate()
    {
        float xSpeedTarget = horizontalInput * maxMoveSpeed;
        float velocityDifference = xSpeedTarget - rb.linearVelocity.x;

        // desired acceleration to achieve target
        float speed = velocityDifference / Time.fixedDeltaTime;
        speed = Mathf.Clamp(speed, -acceleration, acceleration);

        //multilplying the speed by the mass of the player
        float forceX = speed * rb.mass;
        rb.AddForce(new Vector2(forceX, 0f), ForceMode2D.Force);
    }

    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, transform.localScale.y / 2 + 0.1f, groundLayerMask);
        return hit.collider != null;
    }


    public void AddPushPullForce(Vector2 force)
    {
        rb.AddForce(force, ForceMode2D.Force);
    }

    IEnumerator GravityController()
    {
        while (true)
        {
            if (rb.linearVelocity.y > 0 || IsGrounded() || isUsingPushPull)
            {
                rb.gravityScale = airGravity;
                yield return null;
            }
            else
            {
                rb.gravityScale = fallGravity;
                yield return null;
            }
        }
    }

    void DetectLedges()
    {
        if (topLedgeCollider.isTriggered || IsGrounded())
        {
            return;
        }
        if (bottomLedgeCollider.isTriggered && rb.linearVelocity.y > 0)
        {
            LedgeHop();
        }
    }

    void LedgeHop()
    {
        transform.position += Vector3.up * ledgeHopSize;
        Debug.Log("Hop");
    }
}

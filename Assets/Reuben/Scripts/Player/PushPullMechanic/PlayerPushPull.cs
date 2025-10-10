using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerController))]
public class PlayerPushPull : MonoBehaviour
{
    private PlayerController playerController;
    private Rigidbody2D rb;

    public float playerMass;
    public Transform mousePointer;
    private Vector3 mousePos;

    public Rigidbody2D currentMetal;
    public float forceMultiplier = 10f;
    public Rigidbody2D playerRb;

    private bool lockCurrentMetal;

    void Start()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        SetMassBasedOnTransformScale();
    }

    void OnEnable()
    {
        GameEvents.OnMouseHoveringOverMetal += OnMouseHoveringOverMetal;
    }

    void OnDisable()
    {
        GameEvents.OnMouseHoveringOverMetal -= OnMouseHoveringOverMetal;
    }

    void Update()
    {
        Cursor.visible = false;

        if (currentMetal == null) return;

        // Push
        if (Input.GetMouseButton(0))
        {
            lockCurrentMetal = true;
            PushPull(push: true);
        }

        // Pull
        if (Input.GetMouseButton(1))
        {
            lockCurrentMetal = true;
            PushPull(push: false);
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
        {
            playerController.isUsingPushPull = false;
            lockCurrentMetal = false;
        }
    }


    void PushPull(bool push)
    {
        if (currentMetal == null) return;
        
        playerController.isUsingPushPull = true;

        float metalMass = currentMetal.mass;

        //do nothing if the player and the metal have the same mass
        if (playerMass == metalMass) return;

        Vector2 direction = GetPushPullDirection();

        if (!push)
        {
            direction = -direction;
        }

        // Move smaller mass
        if (playerMass < metalMass)
        {
            //clamping the mass ration of the metal and the player becase it was really uncontrollable in the game
            float clampedForce = Mathf.Clamp(metalMass / playerMass, 0f, 5f);
            Vector2 force = forceMultiplier * clampedForce * -direction;
            //applying the force in the player controller script so that forces added to the player are centralized
            playerController.AddPushPullForce(force);
        }
        else
        {
            float clampedForce = Mathf.Clamp(playerMass / metalMass, 0f, 5f);
            Vector2 force = forceMultiplier * clampedForce * direction;
            //applying the force to the metal
            currentMetal.AddForce(force, ForceMode2D.Force);
        }
    }

    Vector2 GetPushPullDirection()
    {
        if (currentMetal == null) return Vector2.zero;
        return (currentMetal.position - playerRb.position).normalized;
    }

    void OnMouseHoveringOverMetal(Metal metal, bool isHovering)
    {
        if (lockCurrentMetal) return;
        currentMetal = metal.rb;
    }

    void SetMassBasedOnTransformScale()
    {
        playerMass = transform.localScale.x * transform.localScale.y;
        rb.mass = playerMass;
    }

    void OnDrawGizmos()
    {
        if (mousePointer != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, mousePos);
            Gizmos.DrawSphere(mousePointer.position, 0.2f);
        }

        if (currentMetal != null && playerRb != null)
        {
            Gizmos.color = Color.yellow;
            Vector2 dir = GetPushPullDirection();
            Gizmos.DrawLine(playerRb.position, playerRb.position + dir * 3f);
        }
    }
}


// void GetMouseHover()
//     {
//         if (lockCurrentMetal) return;

//         RaycastHit2D hit;

//         //check if there is a metal on the mouse position
//         if (Physics2D.Raycast(mousePos, Vector2.zero, 0f))
//         {
//             hit = Physics2D.Raycast(mousePos, Vector2.zero, 0f);
//             if (hit.transform.GetComponent<Metal>())
//             {
//                 //if there is metal, invoke OnHoveringOverMetal event
//                 GameEvents.OnHoveringOverMetal?.Invoke(hit.transform.GetComponent<Metal>(), isHovering: true);
//                 //set the current metal for pushing/pulling 
//                 currentMetal = hit.transform.GetComponent<Rigidbody2D>();
//             }
//             else
//             {
//                 GameEvents.OnHoveringOverMetal?.Invoke(null, isHovering: false);
//                 currentMetal = null;
//             }
//         }
//     }


//Vector2 pushPullDirection;
    // void Update()
    // {
    //     Cursor.visible = false;

    //     MousePosition();

    //     // check if the player is hovering over a metal
    //     GetMouseHover();

    //     //Push
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         //if the player is hovering over a metal and the left mousebutton is pressed
    //         if (currentMetal != null)
    //         {
    //             // //figuring out the leftover mass between the player and the metal to deternmine the force of push/pull
    //             // var pushPullForce = playerMass - currentMetal.mass;
    //             // bool isPlayerMassLarger = playerMass > currentMetal.mass;

    //             // pushPullForce *= pushPullMultiplier;

    //             // //if the players mass is larger than the metal mass- push the metal
    //             // if (isPlayerMassLarger)
    //             // {
    //             //     currentMetal.rb.AddForce(GetPushPullDirection() * -pushPullForce, ForceMode2D.Impulse);
    //             // }
    //             // else
    //             // {
    //             //     //if the players mass is not larger than the metals mass - pull the metal
    //             //     Vector2 flippedDirection = GetPushPullDirection() * -1;
    //             //     rb.AddForce(flippedDirection * pushPullForce, ForceMode2D.Impulse);
    //             // }

    //             pushPullDirection = GetPushPullDirection();

    //             bool isplayerMassLarger = playerMass > currentMetal.mass;

    //             if (isplayerMassLarger)
    //             {
    //                 Debug.Log("metal mass smaller than player mass");
    //                 rb.AddForce(pushPullDirection * pushPullMultiplier, ForceMode2D.Impulse);
    //             }
    //             else
    //             {
    //                 Debug.Log("metal mass larger than player mass");
    //                 pushPullDirection = FlipDirection(pushPullDirection);
    //                 rb.AddForce(pushPullDirection * pushPullMultiplier, ForceMode2D.Impulse);
    //             } 

                
    //         }
    //     }

    //     //Pull
    //     if (Input.GetMouseButtonDown(1))
    //     {
    //         // //if the player is hovering over a metal and the right mouse button is pressed
    //         // var pushPullForce = playerMass - currentMetal.mass;
    //         // bool isPlayerMassLarger = playerMass > currentMetal.mass;

    //         // pushPullForce *= pushPullMultiplier;

    //         // //if the player's mass is larger than the metals mass - pull the metal
    //         // if (isPlayerMassLarger)
    //         // {
    //         //     currentMetal.rb.AddForce(GetPushPullDirection() * pushPullForce, ForceMode2D.Impulse);
    //         // }
    //         // else
    //         // {
    //         //     //if the player's mass is not larger than the metals mass, push the metal
    //         //     Vector2 flippedDirection = GetPushPullDirection();
    //         //     rb.AddForce(flippedDirection * pushPullForce, ForceMode2D.Impulse);
    //         // }

    //         pushPullDirection = GetPushPullDirection();

    //         bool isplayerMassLarger = playerMass > currentMetal.mass;

    //         if (!isplayerMassLarger)
    //         {
    //             pushPullDirection = FlipDirection(pushPullDirection);
    //             currentMetal.rb.AddForce(pushPullDirection * pushPullMultiplier, ForceMode2D.Impulse);
    //         }
    //         else
    //         {
    //             rb.AddForce(pushPullDirection * pushPullMultiplier, ForceMode2D.Impulse);
    //         }

    //     }
    // }
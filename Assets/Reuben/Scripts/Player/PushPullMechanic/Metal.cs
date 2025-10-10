using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Metal : MonoBehaviour
{
    public Rigidbody2D rb;
    public float mass;

    [SerializeField] private Material mouseHoverMat;
    [SerializeField] private Material metalMat;

    public bool isHovered;

    public float hoverDistanceThreshold;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetMassBasedOnTransformScale();
    }

    void OnEnable()
    {
        GameEvents.OnMouseHoveringOverMetal += OnMouseHover;
    }

    void OnDisable()
    {
        GameEvents.OnMouseHoveringOverMetal -= OnMouseHover;
    }
    
    //called when the mouse hovers over a metal in the OnMouseHoveringOverMetal event
    // changes the material of the metal
    void OnMouseHover(Metal metal, bool isHovering)
    {
        if (metal == this && isHovering)
        {
            GetComponent<SpriteRenderer>().material = mouseHoverMat;
        }
        else
        {
            GetComponent<SpriteRenderer>().material = metalMat;
        }
    }

    void SetMassBasedOnTransformScale()
    {
        mass = transform.localScale.x * transform.localScale.y;
        rb.mass = mass;
    }
}

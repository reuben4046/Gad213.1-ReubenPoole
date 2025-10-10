using UnityEngine;

public class LedgeDetection : MonoBehaviour
{
    public bool isTriggered;

    void OnTriggerEnter2D(Collider2D collision)
    {
        isTriggered = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        isTriggered = false;
    }
}

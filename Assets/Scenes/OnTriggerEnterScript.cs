using UnityEngine;

public class OnTriggerEnterScript : MonoBehaviour
{
    public bool triggeredEntered;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        triggeredEntered = true;
        Debug.Log("Triggered");
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        triggeredEntered = false;
        Debug.Log("Exited");
    }
}

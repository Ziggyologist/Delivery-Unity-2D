using UnityEngine;

public class Collision : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision happened with " + collision.gameObject.name);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("trigger happened with: " + collision.gameObject.name);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("exit the trigger with: " + collision.gameObject.name);   
    }
}

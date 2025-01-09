using UnityEngine;

public class Delivery : MonoBehaviour
{
    [SerializeField] float destroyDelay = 0.5f;
    bool hasPackage = false;

    void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision happened with " + collision.gameObject.name);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="Package" && hasPackage == false)
        {
            Debug.Log("Package picked up!");
            hasPackage = true;
            Destroy(collision.gameObject, destroyDelay);
            return;
        }
       else if (collision.tag == "Package" && hasPackage == true)
        {
            Debug.Log("You are already carrying a package. Deliver it and then come back.");
            hasPackage = true;
            return;
        }
        else if (collision.tag == "Customer" && hasPackage == true)
        {
            Debug.Log("Package delivered!");
            hasPackage = false;
            return;
        }
        else
        {
            Debug.Log("You have to pick package first.");
        }
    }

}

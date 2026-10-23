using JetBrains.Annotations;
using UnityEngine;

public class FruitData : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int pickupRequirement = 0;
    public int currentTomotrophs = 0;

    // Update is called once per frame
    void Update()
    {
        if (currentTomotrophs >= pickupRequirement)
        {
            InventoryManager.collectedFruits.Add("Apple");
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tomotroph"))
        {
            currentTomotrophs++;
            Debug.Log("Tomotroph entered");
        }
    }
        void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tomotroph"))
        {
            currentTomotrophs--;
            Debug.Log("Tomotroph exited");
        }
    }
}

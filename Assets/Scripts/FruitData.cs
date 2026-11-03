using System;
using TMPro;
using UnityEditor.Callbacks;
using UnityEngine;

public class FruitData : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int pickupRequirement = 0;
    public int currentTomotrophs = 0;
    public TextMeshProUGUI requirementText;
    public Rigidbody rb;
    public String fruitCode;

    // Update is called once per frame
    private void Start()
    {
        UpdateText();
    }

    void FixedUpdate()
    {
        if (currentTomotrophs >= pickupRequirement)
        {
            InventoryManager.collectedFruits.Add(fruitCode);
            Destroy(gameObject);
        }

        rb.linearVelocity = new Vector3(0f, 0f, 0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tomotroph"))
        {
            currentTomotrophs++;
            UpdateText();
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tomotroph"))
        {
            currentTomotrophs--;
            UpdateText();
        }
    }
    public void UpdateText()
    {
        requirementText.text = currentTomotrophs.ToString() + "/" + pickupRequirement;
    } 
}

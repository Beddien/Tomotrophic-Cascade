using System;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class FruitData : MonoBehaviour
{
    public int pickupRequirement = 0;
    public int currentTomotrophs = 0;
    public TextMeshProUGUI requirementText;
    public Rigidbody rb;
    public String fruitCode;
    [SerializeField] private float minScale = 0f;
    [SerializeField] private float maxScale = 0.07f;

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

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Tomotroph"))
        {
            currentTomotrophs++;
            UpdateText();
        }
    }
        
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Tomotroph"))
        {
            currentTomotrophs--;
            UpdateText();
        }
    }
    public void UpdateText()
    {
        requirementText.text = currentTomotrophs.ToString() + "/" + pickupRequirement;
        float textScaleX = Mathf.Clamp(requirementText.transform.lossyScale.x, minScale, maxScale);
        float textScaleY = Mathf.Clamp(requirementText.transform.lossyScale.y, minScale, maxScale);
        float textScaleZ = Mathf.Clamp(requirementText.transform.lossyScale.z, minScale, maxScale);

        requirementText.transform.localScale = new UnityEngine.Vector3(textScaleX, textScaleY, textScaleZ);
    } 
}

using TMPro;
using UnityEngine;

public class FruitData : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int pickupRequirement = 0;
    public int currentTomotrophs = 0;
    public TextMeshProUGUI requirementText;

    // Update is called once per frame
    private void Start()
    {
        UpdateText();
    }

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

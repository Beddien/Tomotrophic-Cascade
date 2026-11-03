using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Sleep : MonoBehaviour
{
    public Button sleepButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void OnTriggerEnter(Collider collider)
    {
        
        if (collider.gameObject.CompareTag("Player"))
        {
            Debug.Log("player entered range");
            sleepButton.gameObject.SetActive(true);
        }
        
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            sleepButton.gameObject.SetActive(false);
        }
    }

    public void SleepAndEat()
    {
        SceneManager.LoadScene("Sleeping");
    }
}

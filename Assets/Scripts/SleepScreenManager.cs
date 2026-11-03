using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepScreenManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NextDay()
    {
        SceneManager.LoadScene("Level");
    }

    public void EndRun()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Eat()
    {
        
    }
}

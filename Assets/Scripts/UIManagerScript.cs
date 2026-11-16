using TMPro;
using UnityEngine;

public class UIManagerScript : MonoBehaviour
{

    public TextMeshProUGUI tomotrophCounter;
    public float count = TomotrophManager.count;


    void OnEnable()
    {
        EventManager.StartListening("AddOneTomotroph", CountUp);
        EventManager.StartListening("SubOneTomotroph", CountDown);
    }
    void OnDisable()
    {
        EventManager.StopListening("AddOneTomotroph", CountUp);
        EventManager.StopListening("SubOneTomotroph", CountDown);
    }
    public void CountUp()
    {
        count++;
        UpdateCounter();
    }

    public void CountDown()
    {
        count--;
        UpdateCounter();
    }

    private void UpdateCounter()
    {
        tomotrophCounter.text = count.ToString();
    }
}

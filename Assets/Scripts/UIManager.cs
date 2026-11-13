using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public TextMeshProUGUI tomotrophCounter;
    int count = TomotrophManager.count;

    void FixedUpdate()
    {
        count = TomotrophManager.count;
        tomotrophCounter.text = count.ToString();
    }
}

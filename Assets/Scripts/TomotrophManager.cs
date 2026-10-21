using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TomotrophManager : MonoBehaviour
{
    private static readonly List<TomotrophBehavior> available = new List<TomotrophBehavior>();
    public static int count = available.Count;

    public static void Register(TomotrophBehavior t)
    {
        if (!available.Contains(t))
        {
            available.Add(t);
        }
    }

    public static void Deregister(TomotrophBehavior t)
    {
        available.Remove(t);
    }

    public static TomotrophBehavior GetClosest(Vector3 playerTransform)
    {
        TomotrophBehavior closest = null;
        float closest_distance = 100000000;
        float get_thrown_range = 30;
        Debug.Log("available list:");
        Debug.Log(available.Count);
        for (int i = 0; i < available.Count; i++)
        {
            Debug.Log(available[i]);
            float current = (available[i].transform.position - playerTransform).sqrMagnitude;
            if (current < closest_distance && current < get_thrown_range && !available[i].isThrown)
            {
                closest = available[i];
                closest_distance = current;
            }
        }
        Debug.Log("closest:");
        Debug.Log(closest);
        return closest;
    }
}

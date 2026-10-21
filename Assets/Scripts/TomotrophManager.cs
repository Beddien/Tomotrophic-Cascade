using System.Collections.Generic;
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
        float closest_distance = float.MaxValue;
        for (int i = 0; i < available.Count; i++)
        {
            float current = (available[i].transform.position - playerTransform).sqrMagnitude;
            if (current > closest_distance)
            {
                closest = available[i];
                closest_distance = current;
            }
        }
        return closest;
    }
}

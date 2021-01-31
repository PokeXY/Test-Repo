using UnityEngine;

public class Patrol1 : MonoBehaviour
{
    public static Transform[] points1;

    void Awake()
    {
        points1 = new Transform[transform.childCount];
        for (int i = 0; i < points1.Length; i++)
        {
            points1[i] = transform.GetChild(i);
        }
    }
}

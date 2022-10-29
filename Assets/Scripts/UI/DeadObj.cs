using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadObj : MonoBehaviour
{
    [SerializeField] private float cnt;
    private void Start()
    {
        Destroy(gameObject,cnt);
    }

    private void OnApplicationQuit()
    {
        Destroy(transform);
    }
}

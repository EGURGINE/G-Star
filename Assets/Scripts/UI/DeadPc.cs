using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadPc : MonoBehaviour
{
    [SerializeField] private float cnt;

    private void Start()
    {
        Destroy(gameObject,cnt);
    }
}

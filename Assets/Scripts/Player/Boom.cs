using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : Bullet
{
    [SerializeField] private GameObject boomRange;

    private void OnDestroy()
    {
        Instantiate(boomRange).transform.position = transform.position;
    }
}

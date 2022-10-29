using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : Bullet
{
    [SerializeField] private GameObject boomRange;

    public override void OnDestroy()
    {
        base.OnDestroy();
        Instantiate(boomRange).transform.position = transform.position;
    }
}

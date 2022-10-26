using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_1 : BasicEnemy
{
    private float x;
    private float y;

    protected override void Move()
    {
        x = Random.Range(-1f, 1f);
        y = Random.Range(-1f, 1f);

        Vector2 dir = new Vector2(x, y).normalized;

        rb.AddForce(dir * spd);
    }
}   

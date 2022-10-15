using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : BasicEnemy
{
    Transform target => GameManager.Instance.player.transform;

    private void FixedUpdate()
    {
        if (!isdead)
            Move();
    }
    protected override void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, target.position,spd * Time.deltaTime);
    }
}
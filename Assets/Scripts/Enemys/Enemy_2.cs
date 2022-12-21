using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : BasicEnemy
{
    Transform target => GameManager.Instance.player.transform;

    public override void Die()
    {
        print("Die");
        StopCoroutine(TargetMove());
        base.Die();
    }
    protected override void Move()
    {
        StartCoroutine(TargetMove());
    }

    private IEnumerator TargetMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.001f);

            transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime / spd);
        }
    }
    private void OnDisable()
    {
        StopCoroutine(TargetMove());
    }
}
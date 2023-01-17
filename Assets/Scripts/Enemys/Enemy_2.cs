using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : BasicEnemy
{
    Transform target;

    private void Awake()
    {
        target = GameManager.Instance.player.transform;
    }

    public override void Die()
    {
        StopCoroutine(TargetMove());
        base.Die();
    }
    protected override void Move()
    {
        StartCoroutine(TargetMove());
    }

    //Å¸°Ù ÃßÀû 
    private IEnumerator TargetMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.001f);
            float angle = Vector2.Angle(target.position, Vector2.up);
            int sign = (Vector3.Cross(target.position, Vector2.up).z > 0) ? -1 : 1;
            transform.rotation = Quaternion.Euler(new Vector3(0,0,angle * sign));

            transform.position = Vector2.MoveTowards(transform.position, target.position, Time.deltaTime / spd);
        }
    }
    private void OnDisable()
    {
        StopCoroutine(TargetMove());
    }
}
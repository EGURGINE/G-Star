using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicEnemy : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] protected float spd;
    [SerializeField] private ParticleSystem deadPc;
    protected Rigidbody2D rb => GetComponent<Rigidbody2D>();
    protected void Start()
    {
        //rb.velocity = Vector3.up*spd;
    }
    private void FixedUpdate()
    {
        Move();
        if (hp<=0)
        {
            Instantiate(deadPc).transform.position = transform.position;
            Destroy(gameObject);
        }
    }
    protected abstract void Move();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            hp -= collision.GetComponent<Bullet>().dmg;
        }
    }
}

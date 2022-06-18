using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public abstract class BasicEnemy : MonoBehaviour
{
    [SerializeField] private int hp;
    [SerializeField] protected float spd;
    [SerializeField] private int score;
    [SerializeField] private ParticleSystem enemyDeadPc;
    [SerializeField] private ParticleSystem playerDeadPc;
    [SerializeField] private GameObject money;
    private bool isHit = false;
    protected Rigidbody2D rb => GetComponent<Rigidbody2D>();
    protected void Start()
    {
        Spawn();
    }
    private void Spawn()
    {
        GetComponent<SpriteRenderer>().DOFade(1,1.5f).OnComplete(()=> 
        {
            isHit = true;
            tag = "Enemy";
            });
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.isGameOver)
        {
            Die();
        }
        Move();
        if (hp<=0)
        {
           Die();
        }
    }
    private void Die()
    {
        Camera.main.DOShakePosition(1,new Vector3(0.04f,0.01f,0),10).OnComplete(()=>Camera.main.transform.position= new Vector3(0,0,-10));

        Instantiate(enemyDeadPc).transform.position = transform.position;
        GameManager.Instance.Score = score;
        for (int i = 0; i < Random.Range(0,4); i++)
        {
            Instantiate(money).transform.position = new Vector2(
                transform.position.x+ Random.Range(-0.2f, 0.2f), transform.position.y+(Random.Range(-0.2f,0.2f)));
        }
        Destroy(gameObject);
    }
    protected abstract void Move();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isHit) return;
        if (collision.CompareTag("Bullet"))
        {
            hp -= collision.GetComponent<Bullet>().dmg;
        }
        if (collision.CompareTag("Player"))
        {
            Instantiate(playerDeadPc).transform.position = collision.transform.position;
            GameManager.Instance.SetDie();
            collision.gameObject.SetActive(false);
        }
    }
}

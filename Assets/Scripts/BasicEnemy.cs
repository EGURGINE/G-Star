using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public abstract class BasicEnemy : Poolable
{
    [SerializeField] private int hp;
    [SerializeField] [Range(0,2000)] protected float spd;
    [SerializeField] private int score;
    [SerializeField] private ParticleSystem enemySpawnPc;
    [SerializeField] private ParticleSystem enemyDeadPc;
    [SerializeField] private GameObject playerDeadPc;
    [SerializeField] private GameObject money;

    private Color startColor;
    public bool isHit = false;
    float cnt = 0;

    protected Rigidbody2D rb => GetComponent<Rigidbody2D>();
    public void SpawnSet()
    {
        Spawn();
        StartCoroutine(SpawnPc());
    }
    IEnumerator SpawnPc()
    {
        yield return new WaitForSeconds(1);
        enemySpawnPc.Play();
    }
    private void Spawn()
    {
        hp = 1;
        cnt = 0;
        GetComponent<SpriteRenderer>().color = new Color(255,0,117,0);
        GetComponent<CircleCollider2D>().enabled = false;
        StartCoroutine(Fade(1.5f));
        //StartCoroutine(SpawnPc());
    }
    IEnumerator Fade(float _time)
    {
        while (cnt < 1)
        {
            cnt += Time.deltaTime / _time;
            GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, (Time.deltaTime / _time));
            yield return null;
        }
        GetComponent<CircleCollider2D>().enabled = true;
        isHit = true;
        Move();
        yield return null;
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.isGameOver||GameManager.Instance.isUpgrade)
        {
            Die();
        }
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
        //DOTween.KillAll(transform);
        Push();
    }
    protected abstract void Move();
    private void OnTriggerEnter2D(Collider2D collision)
    {
    
        if (collision.CompareTag("Bullet"))
        {
            hp -= collision.GetComponent<Bullet>().dmg;
        }
        if (collision.CompareTag("Player"))
        {
            //playerDeadPc.SetActive(true);
            GameManager.Instance.SetDie();
            collision.gameObject.SetActive(false);
        }
    }
}

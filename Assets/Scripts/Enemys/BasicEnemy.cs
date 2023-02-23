using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public abstract class BasicEnemy : MonoBehaviour, ObserverPattern.IObserver
{
    [SerializeField] private int maxHp;
    [SerializeField] private int hp;
    [SerializeField] [Range(0, 2000)] protected float spd;
    [SerializeField] private int score;
    [SerializeField] private ParticleSystem enemySpawnPc;
    [SerializeField] private ParticleSystem enemyDeadPc;
    [SerializeField] private GameObject playerDeadPc;
    private string money = "Money";

    [SerializeField] private Color startColor;
    public bool isHit = false;
    float cnt = 0;

    protected bool isDead;
    protected Rigidbody2D rb => GetComponent<Rigidbody2D>();
    public void SpawnSet()
    {
        Spawn();
        StartCoroutine(SpawnPc());
    }
    IEnumerator SpawnPc()
    {
        yield return new WaitForSeconds(1);
        ParticleSystem pc = Instantiate(enemySpawnPc);
        pc.transform.position = transform.position;
        pc.Play();
    }
    private void Spawn()
    {
        hp = maxHp;
        cnt = 0;
        GetComponent<SpriteRenderer>().color = startColor;
        GetComponent<PolygonCollider2D>().enabled = false;
        StartCoroutine(Fade(1.5f));
        //본인을 옵저버로 등록한다.
        GameManager.Instance.observerManager.ResisterObserver(this);
    }
    IEnumerator Fade(float _time)
    {
        while (cnt < 1)
        {
            cnt += Time.deltaTime / _time;
            GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, (Time.deltaTime / _time));
            yield return null;
        }
        GetComponent<PolygonCollider2D>().enabled = true;
        isHit = true;
        Move();
        isDead = false;
        yield return null;
    }
    private void FixedUpdate()
    {
        if (isHit && isDead == false && rb.velocity == Vector2.zero)
        {
            Move();
        }
        if (hp <= 0)
        {
            SoundManager.Instance.PlaySound(ESoundSources.DIE);
            Die();
        }
    }

    public void PushObj()
    {
        isDead = true;
        Spawner.Instance.Push(gameObject);
    }
    public virtual void Die()
    {
        if (GameManager.Instance.isTutorial && GameManager.Instance.tutorialNum == 1)
            GameManager.Instance.tutorialNextBtn.gameObject.SetActive(true);

        Instantiate(enemyDeadPc).transform.position = transform.position;

        if (GameManager.Instance.isGameOver == false && GameManager.Instance.isUpgrade == false)
        {
            CameraSetting.Instance.Shake();

            GameManager.Instance.Score += score;

        }

        if (GameManager.Instance.tutorialNum != 1)
        {
            for (int i = 0; i < Random.Range(0, 4); i++)
            {
                Spawner.Instance.Pop(money, new Vector2(transform.position.x, transform.position.y));
            }
        }
        PushObj();
        //죽을시 옵저버를 해지한다.
        GameManager.Instance.observerManager.RemoveObserver(this);
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
            GameManager.Instance.SetRevive();
        }
        if (collision.CompareTag("Boom"))
        {
            hp -= maxHp;
        }
    }

    public void DestroyObj()
    {
        PushObj();
    }
}

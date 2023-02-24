using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Money : MonoBehaviour, ObserverPattern.IObserver
{
    [SerializeField] private float spd;
    private Transform startTr;
    private Transform endTr;
    private Vector3[] m_Points = new Vector3[4];
    private SpriteRenderer SR;
    private Color sColor = new Color(0, 255, 255, 1);
    private float m_timerMax = 0;
    private float m_timerCurrent = 0;


    private bool isHit;


    private void Awake()
    {
        startTr = transform;
        endTr = GameManager.Instance.player.transform;
        SR = GetComponent<SpriteRenderer>();
    }

    public void OnEnableObj()
    {
        GameManager.Instance.observerManager.ResisterObserver(this);
        SR.color = sColor;
        SR.DOFade(0, 4).OnComplete(Die);
        m_timerCurrent = 0;
        isHit = false;
    }

    public void Init()
    {
        isHit = true;
        m_timerMax = Random.Range(0.8f, 1.0f);
        m_Points[0] = startTr.position;
        m_Points[1] = (startTr.position + (Random.Range(-1.5f, 1.5f) * startTr.right) + (Random.Range(-1.5f, 1.5f) * startTr.up));
        m_Points[2] = (endTr.position + (Random.Range(-1.5f, 1.5f) * endTr.right) + (Random.Range(-1.5f, 1.5f) * endTr.up));
        m_Points[3] = endTr.position;

        transform.position = startTr.position;
    }

    private float CuvicVezierCurve(float a, float b, float c, float d)
    {
        float t = m_timerCurrent / m_timerMax;

        float ab = Mathf.Lerp(a, b, t);
        float bc = Mathf.Lerp(b, c, t);
        float cd = Mathf.Lerp(c, d, t);

        float abbc = Mathf.Lerp(ab, bc, t);
        float bccd = Mathf.Lerp(bc, cd, t);
        return Mathf.Lerp(abbc, bccd, t);
    }

    void Update()
    {
        transform.Rotate(0, 0, 3f);

        if (isHit == false || m_timerCurrent > m_timerMax) return;

        m_timerCurrent += Time.deltaTime * spd;

        transform.position = new Vector3(
            CuvicVezierCurve(m_Points[0].x, m_Points[1].x, m_Points[2].x, m_Points[3].x),
            CuvicVezierCurve(m_Points[0].y, m_Points[1].y, m_Points[2].y, m_Points[3].y),
            CuvicVezierCurve(m_Points[0].z, m_Points[1].z, m_Points[2].z, m_Points[3].z));
    }
    private void OnDestroy()
    {
        transform.DOKill();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.Instance.PlaySound(ESoundSources.MONEY);
            GameManager.Instance.Exp += 5;
            GameManager.Instance.Money += 1;
            GameManager.Instance.observerManager.RemoveObserver(this);
            Die();
        }
    }
    private void Die()
    {
        Spawner.Instance.Push(this.gameObject);
    }
    public void DestroyObj()
    {
        Die();
    }
}

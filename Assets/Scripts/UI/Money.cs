using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Money : MonoBehaviour,ObserverPattern.IObserver
{
    [SerializeField] private float spd;
    private Transform startTr => this.transform;
    private Transform endTr => GameManager.Instance.player.transform;
    Vector3[] m_Points = new Vector3[4];

    private float m_timerMax = 0;
    private float m_timerCurrent = 0;

    float cnt;
    private void Start()
    {
        GameManager.Instance.observerManager.ResisterObserver(this);
        StartCoroutine(Fade(4f));
    }
    IEnumerator Fade(float _time)
    {
        while (cnt<1)
        {
            cnt+=Time.deltaTime/_time;
            GetComponent<SpriteRenderer>().color += new Color(0, 0, 0, -(Time.deltaTime / _time)); 
            yield return null;
        }
        Destroy(gameObject);
        yield return null;
    }

    public void Init()
    {
        m_timerMax = Random.RandomRange(0.8f, 1.0f);
        m_Points[0] = startTr.position;
        m_Points[1] = (startTr.position + (Random.Range(-1.5f,1.5f) * startTr.right) + (Random.Range(-1.5f, 1.5f) * startTr.up));
        m_Points[2] = (endTr.position +   (Random.Range(-1.5f,1.5f) * endTr.right)   + (Random.Range(-1.5f, 1.5f) * endTr.up));
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
        return Mathf.Lerp(abbc,bccd,t);
    }

    void Update()
    {
        transform.Rotate(0, 0, 3f);

        if (GameManager.Instance.isGameOver||GameManager.Instance.isUpgrade)
        {
        }

        if (m_timerCurrent > m_timerMax) return;

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
            GameManager.Instance.Money += 1;
            GameManager.Instance.Exp += 5;
            Destroy(gameObject);
        }
    }

    public void DestroyObj()
    {
        GameManager.Instance.Money += 1;
        Destroy(gameObject);
    }
}

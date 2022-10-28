using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Money : MonoBehaviour
{
    float cnt;
    private void Start()
    {
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
    void FixedUpdate()
    {
        transform.Rotate(0, 0, 0.1f);
        if (GameManager.Instance.isGameOver||GameManager.Instance.isUpgrade)
        {
            GameManager.Instance.Money += 1;
            Destroy(gameObject);
        }
            
    }
    private void OnDestroy()
    {
        DOTween.Kill(transform);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            transform.DOMove(collision.transform.position, 0.1f).OnComplete(() =>
            {
                SoundManager.Instance.PlaySound(ESoundSources.MONEY);
                GameManager.Instance.Money += 1;
                GameManager.Instance.Exp += 5;
                transform.DOKill();
                Destroy(gameObject);
            });
        }
    }
}

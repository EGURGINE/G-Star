using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Money : MonoBehaviour
{
    private void Start()
    {
        GetComponent<SpriteRenderer>().DOFade(0, 4).OnComplete(() => Destroy(gameObject));
    }
    void Update()
    {
        transform.Rotate(0, 0, 0.1f);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            GameManager.Instance.Money = 1;
            Destroy(gameObject);
        }
    }
}

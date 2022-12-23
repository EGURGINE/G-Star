using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyRange : MonoBehaviour
{
    [SerializeField] private Money my;
    private bool isCheck = false;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && isCheck == false)
        {
            isCheck = true;
            my.Init();
        }
    }

    private void OnEnable()
    {
        isCheck = false;
    }
}

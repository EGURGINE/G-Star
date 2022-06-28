using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] Transform spawnPos;

    private void Start()
    {
        InvokeRepeating("Spawn", 0, 3);
    }
    void Spawn()
    {
        if (GameManager.Instance.isGameOver || GameManager.Instance.isUpgrade) return;
        for (int i = 0; i < 6; i++)
        {
            spawnPos.position = new Vector2(Random.Range(-2.03f, 2.03f), Random.Range(-2.7f, 3.04f));
            ObjectPoolManager.Instance.pool.Pop().transform.position = spawnPos.position;
        }
    }
}

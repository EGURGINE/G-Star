using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] Enemys;

    private void Start()
    {
        InvokeRepeating("Spawn", 0, 6);
    }
    void Spawn()
    {
        for (int i = 0; i < 8; i++)
        {
            Instantiate(Enemys[Random.Range(0,2)]).transform.position = new Vector2(Random.Range(-2.03f, 2.03f), Random.Range(-2.7f, 2.7f)); 
        }
    }
}

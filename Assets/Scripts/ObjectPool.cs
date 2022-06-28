using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private Poolable enemys;
    [SerializeField] private int enemyCount;

    private Stack<Poolable> poolStack = new Stack<Poolable>();

    private void Start()
    {
        EnemyCate();
    }
    public void EnemyCate()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            Poolable enemyObj = Instantiate(enemys, this.gameObject.transform);
            enemyObj.Create(this);
            poolStack.Push(enemyObj);
        }
    }

    public GameObject Pop()
    {
        Poolable obj = poolStack.Pop();
        obj.gameObject.SetActive(true);
        obj.GetComponent<BasicEnemy>().SpawnSet();
        return obj.gameObject;
    }

    public void Push(Poolable obj)
    {
        obj.gameObject.SetActive(false);
        poolStack.Push(obj);
    }
}

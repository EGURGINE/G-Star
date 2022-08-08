using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EEnemyType
{
    Enemy_1,
    Enemy_2,
    Enemy_3,
    End
}
public class EnemySpawner : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    private static EnemySpawner instance;
    public static EnemySpawner Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
    #endregion

    [SerializeField] private GameObject[] poolEnemys;
    [SerializeField] private int poolCnt;
    Dictionary<object, Stack<GameObject>> poolObjs = new Dictionary<object, Stack<GameObject>>();
    [SerializeField] Transform spawnPos;
    [SerializeField] GameObject Objs;

    private void Start()
    {
        for (int i = 0; i < poolEnemys.Length; i++)
        {
            for (int j = 0; j < poolCnt; j++)
            {
                if (!poolObjs.ContainsKey(poolEnemys[i].name))
                {
                    Stack<GameObject> NewList = new Stack<GameObject>();
                    poolObjs.Add(poolEnemys[i].name, NewList);
                }
                CreateObj(poolEnemys[i]);
            }
        }



        InvokeRepeating("Spawn", 0, 3);
    }
    private void CreateObj(GameObject _obj)
    {
        GameObject newDoll = Instantiate(_obj);
        int index = newDoll.name.IndexOf("(Clone)");
        if (index > 0) newDoll.name = newDoll.name.Substring(0, index);
        newDoll.SetActive(false);
        poolObjs[_obj.name].Push(newDoll);
        newDoll.transform.parent = Objs.transform;
    }
    private void Pop(string _name)
    {
        if (poolObjs[_name].Count == 0)
        {
            print("null");
            foreach (var item in poolEnemys)
            {
                if (item.name == _name)
                {
                    CreateObj(item);
                }
            }
        }
        if (poolObjs.ContainsKey(_name))
        {
            GameObject enemy = poolObjs[_name].Pop();
            enemy.gameObject.SetActive(true);
            enemy.transform.parent = null;
            enemy.transform.transform.position = spawnPos.position;
            enemy.GetComponent<BasicEnemy>().SpawnSet();
        }
    }

    public void Push(GameObject _this)
    {
        poolObjs[_this.name].Push(_this);
        _this.transform.parent = Objs.transform;
        _this.SetActive(false);
    }
    void Spawn()
    {
        if (GameManager.Instance.isGameOver || GameManager.Instance.isUpgrade) return;
        for (int i = 0; i < 6; i++)
        {
            spawnPos.position = new Vector2(Random.Range(-2.03f, 2.03f), Random.Range(-2.7f, 3.04f));
            EEnemyType _name = (EEnemyType)Random.Range(0, (int)EEnemyType.End);
            Pop(_name.ToString());
        }
    }
}

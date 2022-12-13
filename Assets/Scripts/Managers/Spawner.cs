using DG.Tweening.CustomPlugins;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EEnemyType
{
    Enemy_1,
    Enemy_2,
    Enemy_3,
    Enemy_4,
    Enemy_5,
    Enemy_6,
    End
}
public class Spawner : Singleton<Spawner>
{
    readonly string moneyName = "Money";
    //Pool Objs
    [SerializeField] private GameObject moneyObj;
    [SerializeField] private GameObject[] poolEnemys;
    [SerializeField] private int poolCnt;
    Dictionary<string, Stack<GameObject>> poolObjs = new Dictionary<string, Stack<GameObject>>();
    [SerializeField] Transform spawnPos;
    [SerializeField] GameObject EnemyObjs;
    [SerializeField] GameObject MoneyObjs;


    public int enemySpawnNum { private get; set; }
    public float enemySpawnTime { private get; set; }
    public float spawnDelay { private get; set; }
    public float spawnEnemyTypeNum{ private get; set; }
    

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
        for (int i = 0; i < poolCnt; i++)
        {
            if (!poolObjs.ContainsKey(moneyObj.name))
            {
                Stack<GameObject> NewList = new Stack<GameObject>();
                poolObjs.Add(moneyObj.name, NewList);
            }
            CreateObj(moneyObj);
        }


    }

    private void CreateObj(GameObject _obj)
    {
        GameObject newDoll = Instantiate(_obj);
        int index = newDoll.name.IndexOf("(Clone)");
        if (index > 0) newDoll.name = newDoll.name.Substring(0, index);
        newDoll.SetActive(false);
        poolObjs[_obj.name].Push(newDoll);
        if(_obj == moneyObj) newDoll.transform.parent = MoneyObjs.transform;
        else newDoll.transform.parent = EnemyObjs.transform;

    }
    public void Pop(string _name , Vector2 _EnemyPos)
    {
        if (poolObjs.ContainsKey(_name))
        {
            if (poolObjs[_name].Count == 0)
            {
                if (_name == moneyName)
                {
                    CreateObj(moneyObj);
                }
                else
                {
                    foreach (var item in poolEnemys)
                    {
                        if (item.name == _name)
                        {
                            CreateObj(item);
                        }
                    }
                }
            }
            GameObject obj = poolObjs[_name].Pop();
            obj.gameObject.SetActive(true);
            obj.transform.parent = null;
            obj.TryGetComponent(out BasicEnemy isEnemy);
            if (isEnemy)
            {
                obj.transform.position = spawnPos.position;
                obj.GetComponent<BasicEnemy>().SpawnSet();
            }
            else
            {
                obj.transform.position = _EnemyPos + new Vector2(Random.Range(-0.15f, 0.15f), Random.Range(-0.15f, 0.15f));
            }
        }
    }
    public void Push(GameObject _this)
    {
        poolObjs[_this.name].Push(_this);
        if(_this.name == moneyName)
            _this.transform.parent = MoneyObjs.transform;
        else _this.transform.parent = EnemyObjs.transform;
        _this.SetActive(false);
    }

    
    private void Update()
    {
        if (GameManager.Instance.isGameOver == false && GameManager.Instance.isUpgrade == false)
        {
            enemySpawnTime += Time.deltaTime;

            if (enemySpawnTime > spawnDelay)
            {
                for (int i = 0; i < enemySpawnNum; i++)
                {
                    spawnPos.position = new Vector2(Random.Range(-2.03f, 2.03f), Random.Range(-2.7f, 3.04f));
                    EEnemyType _name = (EEnemyType)Random.Range(0, spawnEnemyTypeNum);
                    Pop(_name.ToString(), Vector2.zero);

                }
                enemySpawnTime = 0;
            }

        }

    }
}

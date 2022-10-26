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
    End
}
public class Spawner : MonoBehaviour
{
    #region ΩÃ±€≈Ê
    private static Spawner instance;
    public static Spawner Instance
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
    readonly string moneyName = "Money";
    [SerializeField] private GameObject moneyObj;
    [SerializeField] private GameObject[] poolEnemys;
    [SerializeField] private int poolCnt;
    Dictionary<object, Stack<GameObject>> poolObjs = new Dictionary<object, Stack<GameObject>>();
    [SerializeField] Transform spawnPos;
    [SerializeField] GameObject EnemyObjs;
    [SerializeField] GameObject MoneyObjs;
    public int spawnEnemyNum;
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
                obj.transform.transform.position = spawnPos.position;
                obj.GetComponent<BasicEnemy>().SpawnSet();
            }
            else obj.transform.position = _EnemyPos + new Vector2(Random.Range(-0.15f,0.15f), Random.Range(-0.15f, 0.15f));
        }
    }

    public void Push(GameObject _this)
    {
        poolObjs[_this.name].Push(_this);
        _this.transform.parent = EnemyObjs.transform;
        _this.SetActive(false);
    }
    public void Spawn()
    {
        if (!GameManager.Instance.isGameOver || !GameManager.Instance.isUpgrade)
        {
            for (int i = 0; i < spawnEnemyNum; i++)
            {
                spawnPos.position = new Vector2(Random.Range(-2.03f, 2.03f), Random.Range(-2.7f, 3.04f));
                EEnemyType _name = (EEnemyType)Random.Range(0, (int)EEnemyType.End);
                Pop(_name.ToString(), Vector2.zero);
            }
            StartCoroutine(NextSpawn());

        }
    }
    public IEnumerator NextSpawn()
    {
        yield return new WaitForSeconds(3f);
        if (GameManager.Instance.isGameOver || GameManager.Instance.isUpgrade) {
            print("ddddddddd");
            yield break;
        }
        else Spawn();
    }
    
}

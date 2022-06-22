using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ShotArea : MonoBehaviour
{
    public static ShotArea Instance { get; set; }
    [SerializeField] private Transform shotPos;
    [SerializeField] private Bullet[] bullets;
    public int dmg;
    public float bulletSpd;
    public float shotSpd;
    private float cnt;
    public List<GameObject> Enemys = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.isGameOver || GameManager.Instance.isUpgrade)
        {
             Enemys.Clear();
            Debug.Log(Enemys[0]);
        }
        if (Enemys != null && cnt >= shotSpd)
        {
            cnt = 0;
            Shot();
        }
        cnt += Time.deltaTime;
    }
    private void Shot()
    {
        if (Enemys.FirstOrDefault() != null&&Enemys[0].GetComponent<BasicEnemy>().isHit)
        {
            shotPos.LookAt(Enemys[0].transform.position);
            Bullet bullet = Instantiate(bullets[0]);
            bullet.transform.position = shotPos.position;
            bullet.SetBullet(dmg, bulletSpd, shotPos.forward);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemys.Add(collision.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemys.Remove(collision.gameObject);
        }
    }
    public void ResetState()
    {
        dmg = 1;
        shotSpd = 0.2f;
        bulletSpd = 5;
        if (Enemys.Count >= 1)
        {
            Enemys.Clear();
            Debug.Log("d");
        }
    }
}

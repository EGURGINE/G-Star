using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotArea : MonoBehaviour
{
    [SerializeField] private GameObject shotPos;
    [SerializeField] private Bullet[] bullets;
    public int dmg;
    public float bulletSpd;
    public float shotSpd;
    private float cnt;
    public List<GameObject> Enemys = new List<GameObject>();

    private void FixedUpdate()
    {
        if (cnt>=shotSpd)
        {
            cnt = 0;
            Shot();
        }
        cnt += Time.deltaTime;
    }
    private void Shot()
    {
        Bullet bullet = Instantiate(bullets[0]);
        bullet.transform.position = shotPos.transform.position;
        bullet.SetBullet(dmg,bulletSpd,Vector3.up);
    }
}

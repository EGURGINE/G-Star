using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class ShotArea : Singleton<ShotArea>
{
    [SerializeField] private Transform shotPos;
    [SerializeField] private Transform leftPos, rightPos;
    [SerializeField] private Bullet[] bullets;
    public int dmg;
    public float bulletSpd;

    public float bulletShotSpd;
    private float bulletCnt;

    private float boomShotSpd;
    private float boomSpd;
    private float boomCnt;

    public List<GameObject> Enemys = new List<GameObject>();
    private GameObject target;

    private void Start()
    {
        boomShotSpd = 3f;
        boomSpd = 3f;
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.isGameOver || GameManager.Instance.isUpgrade)
        {
            Enemys.Clear();
        }
        DistanceEnemy();
        if ( bulletCnt >= bulletShotSpd)
        {
            bulletCnt = 0;
            Shot();
        }
        bulletCnt += Time.deltaTime;

        if (PlayerData.Instance.data[PlayerSkills.Boom] == true)  
        {
            if (boomCnt >= boomShotSpd)
            {
                boomCnt = 0;
                BoomShot();
            }
            boomCnt += Time.deltaTime;
        }
    }
    private GameObject DistanceEnemy()
    {
        var neareastObject = Enemys
        .OrderBy(obj =>
        {
            return Vector3.Distance(transform.position, obj.transform.position);
        }).FirstOrDefault();

        return neareastObject;
    }

    private void BoomShot()
    {
        Vector3 randDir = new Vector3(Random.Range(-180, 180), Random.Range(-180, 180), 0);

        Bullet bullet = Instantiate(bullets[1]);
        bullet.transform.position = shotPos.position;
        bullet.SetBullet(0, boomSpd, randDir);
    }
    private void Shot()
    {
        if (Enemys.FirstOrDefault() != null && target.GetComponent<BasicEnemy>().isHit)
        {


            shotPos.LookAt(target.transform.position);
            leftPos.Rotate(new Vector3(30, 0, 0));
            rightPos.Rotate(new Vector3(-30, 0, 0));

            BulletSet();
            if (PlayerData.Instance.data[PlayerSkills.doubleShot])
            {
                Invoke("MultiShot",0.2f);
            }
            if (PlayerData.Instance.data[PlayerSkills.BackShot])
            {
                BackShot();
            }
        }
    }
    private void BulletSet()
    {
        SoundManager.Instance.PlaySound(ESoundSources.SHOT);

        if (PlayerData.Instance.data[PlayerSkills.QuadShot] && PlayerData.Instance.data[PlayerSkills.doubleBullet])
        {
            for (int i = 1; i <= 3; i++)
            {
                Bullet RightBullet = Instantiate(bullets[0]);
                RightBullet.transform.position = shotPos.position + new Vector3(0.2f * i,0);
                RightBullet.SetBullet(dmg, bulletSpd, shotPos.forward);

                Bullet LeftBullet = Instantiate(bullets[0]);
                LeftBullet.transform.position = shotPos.position + new Vector3(-0.2f * i, 0);
                LeftBullet.SetBullet(dmg, bulletSpd, shotPos.forward);
            }
        }
        else if (PlayerData.Instance.data[PlayerSkills.QuadShot])
        {
            for (int i = 1; i <= 2; i++)
            {
                Bullet RightBullet = Instantiate(bullets[0]);
                RightBullet.transform.position = shotPos.position + new Vector3(0.2f * i, 0);
                RightBullet.SetBullet(dmg, bulletSpd, shotPos.forward);

                Bullet LeftBullet = Instantiate(bullets[0]);
                LeftBullet.transform.position = shotPos.position + new Vector3(-0.2f * i, 0);
                LeftBullet.SetBullet(dmg, bulletSpd, shotPos.forward);
            }
        }
        else if (PlayerData.Instance.data[PlayerSkills.doubleBullet])
        {
            for (int i = 0; i < 2; i++)
            {
                Bullet bullet = Instantiate(bullets[0]);
                bullet.transform.position = shotPos.position + new Vector3(-0.2f + i * 0.4f, 0, 0);
                bullet.SetBullet(dmg, bulletSpd, shotPos.forward);
            }
        }
        else
        {
            Bullet bullet = Instantiate(bullets[0]);
            bullet.transform.position = shotPos.position;
            bullet.SetBullet(dmg, bulletSpd, shotPos.forward);
        }

        if (PlayerData.Instance.data[PlayerSkills.RadialShot])
        {
            Bullet bullet_1 = Instantiate(bullets[0]);
            bullet_1.transform.position = shotPos.position;
            bullet_1.SetBullet(dmg, bulletSpd, leftPos.forward);

            Bullet bullet_2 = Instantiate(bullets[0]);
            bullet_2.transform.position = shotPos.position;
            bullet_2.SetBullet(dmg, bulletSpd, rightPos.forward);
        }
       
    }
    private void BackShot()
    {
        Bullet bullet = Instantiate(bullets[0]);
        bullet.transform.position = shotPos.position;
        shotPos.Rotate(0, 180, 0);
        bullet.SetBullet(dmg, bulletSpd, shotPos.forward);
    }
    private void MultiShot()
    {
        BulletSet();
    }
    public void ShotRangeUp()
    {
        transform.localScale = new Vector3(2f, 2f, 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Enemys.Add(collision.gameObject);
            if (target == null) target = collision.gameObject;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            target = DistanceEnemy();
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
        transform.localScale = new Vector3(1.5f ,1.5f,1);
        dmg = 1;
        bulletShotSpd = 0.2f;
        bulletSpd = 5;
        if (Enemys.Count >= 1)
        {
            Enemys.Clear();
        }
    }
}

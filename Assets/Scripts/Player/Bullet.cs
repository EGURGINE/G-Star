using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private ParticleSystem deadPc;
    public int dmg;
    float spd;
    Vector3 dir;
    Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private void Start()
    {
        rb.velocity = dir * spd;
        Destroy(gameObject, 1f);
    }
    public void SetBullet(int _dmg, float _spd, Vector3 _dir)
    {
        dmg = _dmg;
        spd = _spd;
        dir = _dir;
    }

    private void OnApplicationQuit()
    {
        Destroy(gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Wall":
                Instantiate(deadPc).transform.position = transform.position;
                Destroy(gameObject);

                break;
            case "Enemy":
                Instantiate(deadPc).transform.position = transform.position;
                if (PlayerData.Instance.data[PlayerSkills.Piercing] == true) return;
                Destroy(gameObject);
                break;
        }
    }
}

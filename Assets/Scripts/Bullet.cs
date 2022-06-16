using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    int dmg;
    float spd;
    Vector3 dir;
    Rigidbody2D rb=>GetComponent<Rigidbody2D>();
    private void Start()
    {
        rb.velocity = dir * spd;
    }
    public void SetBullet(int _dmg, float _spd, Vector3 _dir)
    {
        dmg = _dmg;
        spd = _spd;
        dir = _dir;
    }
}

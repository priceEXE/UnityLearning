using System;
using UnityEngine;

public class EnamyBullet : Bullet
{
    private int damage;

    // Start is called before the first frame update
    void Awake()
    {
        speed = 2f;
        LifeTime = 3f;
        isEnamyBullet = true;
        damage = 1;
    }

    public override float getSpeed()
    {
        return speed;
    }

    public int GetDamage()
    {
        return damage;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            
        }
    }
    // Update is called once per frame
    void Update()
    {
        TimeTick();
    }
}

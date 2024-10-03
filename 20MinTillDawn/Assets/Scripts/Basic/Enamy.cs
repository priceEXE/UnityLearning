using System.Xml.Serialization;
using UnityEngine;

public abstract class Enamy : MonoBehaviour
{
    public int health;
    public int damage;
    public float speed;
    public float attackFre;
    public float attackRange;
    public float moveRange;
    public Collider2D HitBox;

    public SpriteRenderer spriteRenderer;
    public abstract void MoveToPlayer();//移动接口
    public abstract void Attack();//攻击接口
    public abstract void Waiting();//待机接口
    public abstract float DetecteDitance();//探测接口
    public void DecreaseHealth(int damage)//扣除血量
    {
        health-=damage;
        if(health == 0) Dead();
    }
    public void Dead()
    {
        Destroy(gameObject);
    }
    
}

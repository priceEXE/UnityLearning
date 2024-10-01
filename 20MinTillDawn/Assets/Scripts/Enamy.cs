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
    public CircleCollider2D HitBox;
    public abstract void MoveToPlayer();
    public abstract void Attack();
    public abstract void Waiting();
    public abstract float DetecteDitance();
    public void DecreaseHealth(int damage)
    {
        health-=damage;
        if(health == 0) Dead();
    }
    public void Dead()
    {
        Destroy(gameObject);
    }
    
}

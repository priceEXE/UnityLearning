using System.Collections;
using UnityEngine;

public abstract class Enamy : MonoBehaviour
{
    public int health;
    public int damage;
    public float speed;
    public float attackFre;
    public float attackRange;
    public float moveRange;

    public int exp;
    public Collider2D HitBox;

    public SpriteRenderer spriteRenderer;
    public GameObject ExpPoint;

    public AudioClip DeathClip;
    public GameObject gameManager;
    public abstract void MoveToPlayer();//移动接口
    public abstract void Attack();//攻击接口
    public abstract void Waiting();//待机接口
    public abstract float DetecteDitance();//探测接口
    public void DecreaseHealth(int damage)//扣除血量
    {
        gameManager.GetComponent<GameManager>().player.AudioHit();
        health-=damage;
        if(health == 0) Dead();
    }
    public virtual void Dead()
    {
        if(gameObject.GetComponent<ShortRangeEnamy>())  gameManager.GetComponent<GameManager>().player.DeathClip = gameObject.GetComponent<ShortRangeEnamy>().DeathClip;
        if(gameObject.GetComponent<LongRangeEnamy>())   gameManager.GetComponent<GameManager>().player.DeathClip = gameObject.GetComponent<LongRangeEnamy>().DeathClip;
        if(gameObject.GetComponent<Bommer>())   gameManager.GetComponent<GameManager>().player.DeathClip = gameObject.GetComponent<Bommer>().DeathClip;
        gameManager.GetComponent<GameManager>().player.AudioDead();
        GameObject point = Instantiate(ExpPoint,GameObject.Find("GameManager").transform);
        point.transform.position = gameObject.transform.position;
        point.GetComponent<ExpPoint>().Setexp(exp);
        Destroy(gameObject);
        GameObject.Find("GameManager").GetComponent<GameManager>().decreaseEnamy();
    }

    
    
}

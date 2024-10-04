using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bommer : Enamy
{
    public GameObject BoomEffect;
    // Start is called before the first frame update
    void Awake()
    {
        health = 5;
        speed = 2.1f;
        attackFre = 10f;
        attackRange = 0.9f;
        moveRange = 10f;
        damage = 1;
        exp = 10;
        gameManager = GameObject.Find("GameManager");
        HitBox = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<PlayerBullet>())   DecreaseHealth(gameManager.GetComponent<GameManager>().player.GetDamage());
        if(other.gameObject.CompareTag("Boom")) Dead();
    }

    //private void OnTriggerStay2D(Collider2D other) {
     //   if(other.gameObject.CompareTag("Boom")) Dead();
    //}

    public override void Waiting()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public override float DetecteDitance()
    {
        return (gameManager.GetComponent<GameManager>().player.transform.position - transform.position).magnitude;
    }

    public override void MoveToPlayer()
    {
        Vector2 target =  gameManager.GetComponent<GameManager>().player.transform.position - transform.position;
        if(target.x < 0)    spriteRenderer.flipX = true;
        else spriteRenderer.flipX = false;
        GetComponent<Rigidbody2D>().velocity = target.normalized * speed; 
    }

    public override void Dead()
    {
        GameObject effect = Instantiate(BoomEffect,gameObject.transform);
        effect.transform.parent = gameManager.transform;
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

    public override void Attack()
    {
        GameObject effect = Instantiate(BoomEffect,gameObject.transform);
        effect.transform.parent = gameManager.transform;
        gameManager.GetComponent<GameManager>().player.DecreaseHealth(damage);
        Dead();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = DetecteDitance();
        if( distance < attackRange)
        {
            Attack();
        }
        else if(distance < moveRange)
        {
            MoveToPlayer();
        }
        else if(distance < MaxDistance)
        {
            Waiting();
        }
        else
        {
            Clear();
        }
    }
}

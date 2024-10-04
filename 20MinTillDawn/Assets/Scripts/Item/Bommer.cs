using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bommer : Enamy
{
    private GameObject gameManager;
    public GameObject BoomEffect;
    // Start is called before the first frame update
    void Awake()
    {
        health = 5;
        speed = 1.05f;
        attackFre = 10f;
        attackRange = 1.5f;
        moveRange = 3f;
        damage = 1;
        exp = 10;
        gameManager = GameObject.Find("GameManager");
        HitBox = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<PlayerBullet>())   DecreaseHealth(gameManager.GetComponent<GameManager>().player.GetDamage());
    }

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

    public override void Attack()
    {
        GameObject effect = Instantiate(BoomEffect,gameObject.transform);
        effect.transform.parent = gameManager.transform;
        gameManager.GetComponent<GameManager>().player.DecreaseHealth(damage);
        Destroy(gameObject); 
    }

    public override void Dead()
    {
        GameObject point = Instantiate(ExpPoint,GameObject.Find("GameManager").transform);
        point.transform.position = gameObject.transform.position;
        point.GetComponent<ExpPoint>().Setexp(exp);
        Destroy(gameObject);
        GameObject.Find("GameManager").GetComponent<GameManager>().decreaseEnamy();
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
        else
        {
            Waiting();
        }
    }
}

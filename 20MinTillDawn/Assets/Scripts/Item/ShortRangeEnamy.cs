using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class ShortRangeEnamy : Enamy
{
    private float timer = 0f;
    public override void MoveToPlayer()
    {
        Vector2 target =  gameManager.GetComponent<GameManager>().player.transform.position - transform.position;
        if(target.x < 0)    spriteRenderer.flipX = true;
        else spriteRenderer.flipX = false;
        GetComponent<Rigidbody2D>().velocity = target.normalized * speed; 
    }
    // Start is called before the first frame update
    public override void Attack()
    {
        Debug.Log("Attck!");
        gameManager.GetComponent<GameManager>().player.DecreaseHealth(damage);
    }
    public override float DetecteDitance()
    {
        return (gameManager.GetComponent<GameManager>().player.transform.position - transform.position).magnitude;
    }
    public override void Waiting()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        
    }
    void Awake()
    {
        health = 5;
        speed = 1f;
        attackFre = 1f;
        attackRange = 0.7f;
        moveRange = 6f;
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
    //    if(other.gameObject.CompareTag("Boom")) Dead();
    //}

    // Update is called once per frame
    void Update()
    {
        if(timer!=0)
        {
            timer+=Time.deltaTime;
            if(timer >= attackFre)  timer = 0;
        }
        float distance = DetecteDitance();
        if(distance>=0 && distance<attackRange && timer==0)
        {
            Attack();
            //gameManager.GetComponent<GameManager>().player.DecreaseHealth(damage);
            Debug.Log("Monster Attack!");
            timer+=Time.deltaTime;
        }
        else if(distance>=attackRange && distance<moveRange)
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

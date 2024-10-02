using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class ShortRangeEnamy : Enamy
{
    private float timer = 0f;
    private GameObject gameManager;
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
        speed = 1.01f;
        attackFre = 1f;
        attackRange = 0.1f;
        moveRange = 1.5f;
        damage = 1;
        gameManager = GameObject.Find("GameManager");
        HitBox = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.GetComponent<PlayerBullet>())   DecreaseHealth(gameManager.GetComponent<GameManager>().player.GetDamage());
    }
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
        else
        {
            Waiting();
        }
    }

}

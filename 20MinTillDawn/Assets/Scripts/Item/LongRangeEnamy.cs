using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeEnamy : Enamy
{
    private float timer = 0f;
    public GameObject bullet;
    
    // Start is called before the first frame update
    void Awake()
    {
        health = 2;
        damage = 1;
        speed = 0.9f;
        attackFre = 0.5f;
        attackRange = 4f;
        moveRange = 6f;
        exp = 10;
        HitBox = GetComponent<CircleCollider2D>();
        gameManager = GameObject.Find("GameManager");
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<PlayerBullet>())   DecreaseHealth(gameManager.GetComponent<GameManager>().player.GetDamage());
        if(other.gameObject.CompareTag("Boom")) Dead();
    }

    //private void OnTriggerStay2D(Collider2D other) {
    //    if(other.gameObject.CompareTag("Boom")) Dead();
    //}

    public override void MoveToPlayer()
    {
        Vector2 target =  gameManager.GetComponent<GameManager>().player.transform.position - transform.position;
        if(target.x < 0)    spriteRenderer.flipX = true;
        else spriteRenderer.flipY = false;
        GetComponent<Rigidbody2D>().velocity = target.normalized * speed; 
    }

    public override float DetecteDitance()
    {
        return (gameManager.GetComponent<GameManager>().player.transform.position - transform.position).magnitude;
    }

    public override void Waiting()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    public override void Attack()
    {
        Vector3 direction = (gameManager.GetComponent<GameManager>().player.transform.position - gameObject.transform.position).normalized;
        GameObject Bullet = Instantiate(bullet,GetComponent<Transform>());
        Bullet.GetComponent<Rigidbody2D>().velocity = Bullet.GetComponent<EnamyBullet>().getSpeed() * direction.normalized;
        Bullet.transform.right = -Bullet.GetComponent<Rigidbody2D>().velocity;
        Bullet.transform.parent = gameManager.transform;
    }

    
    // Update is called once per frame
    void Update()
    {
        if(timer!=0)
        {
            timer+=Time.deltaTime;
            if(timer>=attackFre)    timer = 0;
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

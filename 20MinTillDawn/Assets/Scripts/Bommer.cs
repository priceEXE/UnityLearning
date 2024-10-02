using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bommer : Enamy
{
    private float timer;
    private GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        speed = 1.05f;
        attackFre = 5f;
        attackRange = 1.5f;
        moveRange = 3f;
        damage = 4;
        gameManager = GameObject.Find("GameManager");
        HitBox = GetComponent<CircleCollider2D>();
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
        GetComponent<Rigidbody2D>().velocity = target.normalized * speed; 
    }

    public override void Attack()
    {
        Destroy(gameObject);
        gameManager.GetComponent<GameManager>().player.DecreaseHealth(damage);
    }
    // Update is called once per frame
    void Update()
    {
        float distance = DetecteDitance();
        timer+=Time.deltaTime;
        if( timer>=attackFre || distance < attackRange)
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

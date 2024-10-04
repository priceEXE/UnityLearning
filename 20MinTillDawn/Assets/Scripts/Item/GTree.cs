using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GTree : Enamy
{
    private float timer = 0f;
    public override void MoveToPlayer()//睁眼
    {
        return;
    }

    public override void Waiting()//闭眼
    {
        return;
    }

    public override float DetecteDitance()
    {
        return (gameManager.GetComponent<GameManager>().player.transform.position - transform.position).magnitude;
    }

    public override void Attack()
    {
        gameManager.GetComponent<GameManager>().player.DecreaseHealth(damage);
    }
    // Start is called before the first frame update
    void Start()
    {
        attackFre = 2f;
        attackRange = 1.5f;
        moveRange = 3f;
        damage = 1;
        gameManager = GameObject.Find("GameManager");
        HitBox = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer>0)
        {
            timer+=Time.deltaTime;
            if(timer>=attackFre)    timer = 0;
        }
        float distance = DetecteDitance();
        if(timer==0 && distance<attackRange)
        {
            Debug.Log("Attacking!");
            Attack();
            timer+=Time.deltaTime;
        }
        else if(distance > attackRange && distance<moveRange)
        {
            MoveToPlayer();
        }
        else
        {
            Waiting();
        }
    }
}

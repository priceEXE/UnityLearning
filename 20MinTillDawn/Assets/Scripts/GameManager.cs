using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private float timer;
    public Bullet bullet;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public Player player;
    // Update is called once per frame
    void Update()
    {
        Vector2 vector = Vector2.zero;
        if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            Debug.Log("get w down");
            Vector2 vectorX = new Vector2(0f,player.GetWalkSpeed());
            vector += vectorX;
        }
        else if(Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            Debug.Log("get s down");
            Vector2 vectorX = new(0f,-player.GetWalkSpeed());
            vector += vectorX;
        }
        if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            Debug.Log("get a down");
            Vector2 vectorY = new(-player.GetWalkSpeed(),0f);
            vector += vectorY;
        }
        else if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            Debug.Log("get d down");
            Vector2 vectorY = new(player.GetWalkSpeed(),0f);
            vector += vectorY;
        }
        player.Walk(ref vector);
        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W))
        {
            player.ClearSpeed();
        }
        if(Input.GetMouseButton(0) && timer == 0f)
        {
            player.ShootBullet();
            timer += Time.deltaTime;
        }
        else if(Input.GetMouseButton(0))
        {
            timer += Time.deltaTime; 
            if(timer >= player.GetShootFre()) timer = 0f;
        }
        else if(timer != 0f)
        {
            timer += Time.deltaTime;
            if(timer >= player.GetShootFre()) timer = 0f;
        }
    }
}

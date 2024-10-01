using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int health;
    private float walkSpeed = 5;
    private int exp;
    private float shootFrequency = 1;
    public GameObject bullet;
    public GameManager gameManager;
    public void ShootBullet()//射击
    {
        Vector3 mousepo = Input.mousePosition;
        Debug.Log(mousepo);
        mousepo.z = Mathf.Abs(Camera.main.transform.position.z);
        mousepo = Camera.main.ScreenToWorldPoint(mousepo); 
        Vector3 transform = GetComponent<Transform>().position;
        GameObject gameObject = Instantiate(bullet,GetComponent<Transform>());
        gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Bullet>().getSpeed() * (mousepo - transform).normalized; 
        gameObject.transform.parent = gameManager.transform;
    }
    
    public void Walk(ref Vector2 vocalotiy)//行走
    {
        GetComponent<Rigidbody2D>().velocity = vocalotiy;
    }
    public void DecreaseHealth(int damage)
    {
        health-=damage;
        if(health == 0) Debug.Log("dead");
    }
    public void ClearSpeed()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    public int GetHealth()//获取生命值UI
    {
        return health;
    }
    public int GetExp()//获取经验值UI
    {
        return exp;
    }
    public float GetWalkSpeed()
    {
        return walkSpeed;
    }
    public float GetShootFre()
    {
        return shootFrequency;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

        
}

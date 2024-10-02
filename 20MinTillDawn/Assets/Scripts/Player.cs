using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int health;
    private int damage;
    private float walkSpeed = 1;
    private int exp = 0;
    private float shootFrequency = 0.2f;
    public GameObject bullet;
    public GameManager gameManager;
    public void ShootBullet()//射击
    {
        Vector3 mousepo = Input.mousePosition;
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
    public void DecreaseHealth(int damage)//扣除角色血量
    {
        Debug.Log("Be attacked!" + damage);
        health-=damage;
        if(health == 0) Dead();
    } 

    public void InhanceExp(int exp)//角色获得经验值
    {
        this.exp += exp;
    }
    public void Dead()
    {
        Time.timeScale = 0f;
    }
    public void ClearSpeed()//清空角色速度
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
    public float GetWalkSpeed()//获取角色行走速度
    {
        return walkSpeed;
    }
    public float GetShootFre()//获取角色攻击频率
    {
        return shootFrequency;
    }
    public int GetDamage()//获取角色攻击力
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.GetComponent<EnamyBullet>())
        {
            DecreaseHealth(other.gameObject.GetComponent<EnamyBullet>().GetDamage());
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        health = 5;
        damage = 1;
    }


        
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    private int health;//生命值
    private int maxHealth;//最大生命值
    private int damage;//伤害
    private float walkSpeed = 1;//行动速度
    public int exp = 0;//累计经验值
    private int maxExp = 50;//最大经验值

    private int Level = 0;//经验等级
    private float shootFrequency = 0.2f;//射击频率

    private int ammo;//满弹药数

    private int magazine;//当前弹药基数

    private float loadTime;//换弹间隔
    private bool isReload = false;
    public GameObject bullet;//子弹预制体

    private Animator animator;
    public GameObject gameManager;//游戏管理器
    private float timer;
    public void ShootBullet()//射击
    {
        Vector3 mousepo = Input.mousePosition;
        mousepo.z = Mathf.Abs(Camera.main.transform.position.z);
        mousepo = Camera.main.ScreenToWorldPoint(mousepo); 
        Vector3 transform = GetComponent<Transform>().position;
        GameObject gameObject = Instantiate(bullet,GetComponent<Transform>());
        gameObject.GetComponent<Rigidbody2D>().velocity = gameObject.GetComponent<Bullet>().getSpeed() * (mousepo - transform).normalized; 
        gameObject.transform.parent = gameManager.transform;
        gameObject.transform.right = -gameObject.GetComponent<Rigidbody2D>().velocity;
        magazine--;

    }
    
    public void Walk(ref Vector2 vocalotiy)//行走
    {
        animator.SetFloat("x",vocalotiy.x);
        GetComponent<Rigidbody2D>().velocity = vocalotiy;
    }
    public void DecreaseHealth(int damage)//扣除角色血量
    {
        Debug.Log("Be attacked!" + damage);
        gameManager.GetComponent<GameManager>().healthLine.DecreaseHealth();
        health-=damage;
        if(health <= 0) Dead();
    } 

    public void IncreaseHealth()//恢复角色血量
    {
        if(health<maxHealth)    {
            health++;
            gameManager.GetComponent<GameManager>().healthLine.IncreaseHealth();
        }
        

    }

    public void InhanceExp(int exp)//角色获得经验值
    {
        this.exp += exp;
        Debug.Log("You dead!");
    }

    public void LevelUp()//升级
    {
        exp-=maxExp;
        maxExp += 10;
        Level++;
        gameManager.GetComponent<GameManager>().level.LevelUp(Level);
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
    public int GetMaxHealth()
    {
        return maxHealth;
    }
    public int GetExp()//获取经验值UI
    {
        return exp;
    }
    public int GetMaxExp()
    {
        return maxExp;
    }
    public int GetAmmo()
    {
        return ammo;
    }
    public int GetMagazine()
    {
        return magazine;
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
    void Awake()
    {
        maxHealth = 10;
        health = 5;
        maxExp = 50;
        exp = 0;
        damage = 1;
        ammo = 15;
        magazine = ammo;
        loadTime = 5f;
        gameManager = GameObject.Find("GameManager");
        animator = GetComponent<Animator>();
    }

    public IEnumerator Reload()
    {
        magazine = 0;
        isReload = true;
        float timer = 0f;
        while(timer <= loadTime)
        {
            timer += Time.deltaTime;
            yield return null;
        }
        magazine = ammo;
        isReload = false;
    }

    void Update()
    {
        Vector2 vector = Vector2.zero;
        if(Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S))
        {
            Vector2 vectorX = new Vector2(0f,walkSpeed);
            vector += vectorX;
        }
        else if(Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.W))
        {
            Vector2 vectorX = new(0f,-walkSpeed);
            vector += vectorX;
        }
        if(Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D))
        {
            Vector2 vectorY = new(-walkSpeed,0f);
            vector += vectorY;
        }
        else if(Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A))
        {
            Vector2 vectorY = new(walkSpeed,0f);
            vector += vectorY;
        }
        Walk(ref vector);
        if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W))
        {
            ClearSpeed();
        }
        if(Input.GetMouseButton(0) && timer == 0f && magazine != 0 && !isReload && Time.timeScale!=0)
        {
            ShootBullet();
            timer += Time.deltaTime;
        }
        else if(Input.GetMouseButton(0) && magazine == 0 && !isReload || Input.GetKeyDown(KeyCode.R) && !isReload && Time.timeScale!=0)
        {
            timer = 0f;
            StartCoroutine(Reload());
        }
        else if(timer != 0f && Time.timeScale!=0)
        {
            timer += Time.deltaTime;
            if(timer >= shootFrequency) timer = 0f;
        }
        if(Input.GetKeyDown(KeyCode.Space))
        {
            exp+=10;
        }
        if(exp>=maxExp)
        {
            LevelUp();
        }
    }
}

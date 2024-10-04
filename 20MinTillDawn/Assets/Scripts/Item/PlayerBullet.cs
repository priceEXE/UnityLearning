using UnityEditor;
using UnityEngine;

public class PlayerBullet : Bullet
{
    // Start is called before the first frame update
    void Awake()
    {
        speed = 5f;
        LifeTime = 3f;
        isEnamyBullet = false;
    }

    
    public override float getSpeed()
    {
        return speed;
    }
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enamy"))
        {
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        TimeTick();
    }
}

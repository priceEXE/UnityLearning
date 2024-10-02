using UnityEngine;

public abstract class Bullet : MonoBehaviour
{
    protected float speed;
    protected float LifeTime;
    protected float timer = 0f;

    protected bool isEnamyBullet = false;
    public abstract float getSpeed();
    protected void TimeTick()
    {
        timer+=Time.deltaTime;
        if(timer >= LifeTime)   Destroy(gameObject);
    }

    protected abstract void OnTriggerEnter2D(Collider2D other);
}

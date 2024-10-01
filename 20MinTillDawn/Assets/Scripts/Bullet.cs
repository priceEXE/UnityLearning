using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed =2f;
    private float LifeTime = 1f;
    private float timer = 0f;
    public float getSpeed()
    {
        return speed;
    }
    private void TimeTick()
    {
        timer+=Time.deltaTime;
        if(timer >= LifeTime)   Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        TimeTick();
    }
}

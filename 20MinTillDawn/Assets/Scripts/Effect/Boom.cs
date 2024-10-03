using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float timer = 0f;
    // Start is called before the first frame update
    void Awake()
    {

    }

    void Update()
    {
        timer+=Time.deltaTime;
        if(timer>=0.4f) Destroy(gameObject);
    }
    
}

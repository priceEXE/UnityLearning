using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float timeTarget;
    public Player player;

    public HealthLine healthLine;

    public Level level;
    public Ammo ammo;
    // Start is called before the first frame update
    void Awake()
    {
        timeTarget = 1200;
        player = GameObject.Find("Player").GetComponent<Player>();
        healthLine = GameObject.Find("Canvas/Health").GetComponent<HealthLine>();
        level = GameObject.Find("Canvas/Level").GetComponent<Level>();
        ammo = GameObject.Find("Canvas/Ammo").GetComponent<Ammo>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(Time.time > timeTarget)
        {
            Debug.Log("Victory");
        }
    }

}

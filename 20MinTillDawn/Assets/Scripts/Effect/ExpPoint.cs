using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpPoint : MonoBehaviour
{
    public int exp;
    private GameManager gameManager;
    public void Setexp(int ini)//根据参数设置经验点数值
    {
        exp = ini;
    }

    void Awake() {
        exp = 0;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
        {
            gameManager.player.InhanceExp(exp);
            Destroy(gameObject);
        }
    }

    
}

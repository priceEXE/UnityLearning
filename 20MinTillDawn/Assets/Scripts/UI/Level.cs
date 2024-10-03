using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour
{
    private GameObject gameManager;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
    }

    public void LevelUp(int Level)
    {
        gameObject.GetComponent<Text>().text = "Level " +Level; 
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

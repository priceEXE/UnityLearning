using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private Text text;
    private GameObject gameManager;
    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponent<Text>();
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        float time = gameManager.GetComponent<GameManager>().timeTarget - Time.time;
        int minutes = (int)time / 60;
        int seconds = (int)time % 60;
        if(minutes != 0 && seconds != 0)
        {
            text.text = minutes.ToString() + ":" + seconds.ToString();
        }
        else if(minutes == 0 && seconds!=0)
        {
            text.text =  "00:" + seconds.ToString();
        }
        else if(minutes != 0 && seconds==0)
        {
            text.text =  minutes.ToString() + ":00";
        }
        else
        {
            text.text = "00:00";
        }
        
    }
}

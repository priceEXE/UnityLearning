using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpLine : MonoBehaviour
{
    private Slider slider;
    private GameObject gameManager;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = gameManager.GetComponent<GameManager>().player.GetExp() * 1.0f / gameManager.GetComponent<GameManager>().player.GetMaxExp();
    }
}

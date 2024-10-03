using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ammo : MonoBehaviour
{
    private GameObject gameManager;
    private Text textContent;
    // Start is called before the first frame update
    void Awake()
    {
        gameManager = GameObject.Find("GameManager");
        textContent = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        textContent.text = gameManager.GetComponent<GameManager>().player.GetMagazine().ToString() + "/" + gameManager.GetComponent<GameManager>().player.GetAmmo().ToString();
    }
}

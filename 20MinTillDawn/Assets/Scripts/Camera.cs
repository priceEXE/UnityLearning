using UnityEngine;
using System.Collections;

public class GameCamera : MonoBehaviour {

    private GameObject gameManager;
    // Use this for initialization
    void Start () {
        gameManager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update () {
        transform.position = gameManager.GetComponent<GameManager>().player.transform.position + new Vector3(0f,0f,-10f);
    }
}

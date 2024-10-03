using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthLine : MonoBehaviour
{
    public Stack stack = new Stack();
    private Vector3 distance = new Vector3(100,0,0);
    public GameObject gameManager;
    public GameObject heart;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        int length = gameManager.GetComponent<GameManager>().player.GetHealth();
        Debug.Log("root =" + length);
        for(int i=0;i<length;i++)
        {
            GameObject gameObject = Instantiate(heart,transform);
            gameObject.transform.position += i * distance;
            stack.Push(gameObject);
        }
    }

    public void DecreaseHealth()
    {
        GameObject gameObject = (GameObject)stack.Peek();
        stack.Pop();
        Destroy(gameObject);
    }

    public void IncreaseHealth()
    {
        GameObject gameObject = Instantiate(heart,transform);
        gameObject.transform.position += stack.Count * distance;
        stack.Push(gameObject);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}

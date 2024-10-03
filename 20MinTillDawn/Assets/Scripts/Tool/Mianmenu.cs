using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mianmenu : MonoBehaviour
{
    public void StartGame()
    {
        StartCoroutine(Load());
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Load()
    {

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Scenes/Game");

        asyncOperation.allowSceneActivation = false;    // 这里限制了跳转


        // 这里就是循环输入进度
        while(asyncOperation.progress < 0.9f)
        {
            Debug.Log(" progress = " + asyncOperation.progress);
            yield return null;

        }

        asyncOperation.allowSceneActivation = true;    // 这里打开限制
    }
}

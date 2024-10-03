using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private bool isGaming;
    public int enamyCounter;

    public float timer;
    public float timeTarget;
    public Player player;

    public HealthLine healthLine;

    public Level level;
    public Ammo ammo;

    private GameObject PausePanel;

    public GameObject ShortRange;
    public GameObject LongRange;
    public GameObject Boomer;
    // Start is called before the first frame update
    void Awake()
    {
        isGaming = true;
        timeTarget = 1200;
        player = GameObject.Find("Player").GetComponent<Player>();
        healthLine = GameObject.Find("Canvas/Health").GetComponent<HealthLine>();
        level = GameObject.Find("Canvas/Level").GetComponent<Level>();
        ammo = GameObject.Find("Canvas/Ammo").GetComponent<Ammo>();
        PausePanel = GameObject.Find("Canvas/Pausepanel");
        PausePanel.SetActive(false);
        enamyCounter = 0;
        timer = 0;
    }
    
    private void GenerateEnamy()
    {
        float a = Random.Range(0f, 6.28f);
        int mod = Random.Range(0,100) % 3;
        float distance = Random.Range(2.5f,5f);
        Vector3 position  = new Vector3(Mathf.Cos(a),Mathf.Sin(a),0);
        if(mod==0)
        {
            Debug.Log("近战");
            GameObject p = Instantiate(ShortRange,transform);
            p.transform.position = distance * position + player.transform.position;
            enamyCounter++;
        }
        else if(mod==1)
        {
            Debug.Log("远程");
            GameObject p = Instantiate(LongRange,transform);
            p.transform.position = distance * position + player.transform.position;
            enamyCounter++;
        }
        else
        {
            Debug.Log("自爆");
            GameObject p = Instantiate(Boomer,transform);
            p.transform.position = distance * position + player.transform.position;
            enamyCounter++;
        }
    }

    public void decreaseEnamy()
    {
        enamyCounter--;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        isGaming = false;
        PausePanel.SetActive(true);

    }

    public void Restart()
    {
        Time.timeScale = 1f;
        isGaming = true;
        PausePanel.SetActive(false);
    }

    public void Return()
    {
        Time.timeScale = 1f;
        StartCoroutine(Load());
    }
    // Update is called once per frame
    void Update()
    {
        if(Time.time > timeTarget)
        {
            Time.timeScale = 0f;
        }

        if(Input.GetKeyDown(KeyCode.Escape) && isGaming)
        {
            Pause();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && !isGaming)
        {
            Restart();
        }

        if(timer == 0 && enamyCounter < 10)
        {
            GenerateEnamy();
        }

        timer += Time.deltaTime;
        if(timer >= 2f)
        {
            timer = 0f;
        }
    }

    IEnumerator Load()
    {

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync("Scenes/Mainmenu");

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

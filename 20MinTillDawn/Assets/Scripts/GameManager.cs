using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public float StartTime;
    private bool isVictory;
    private bool isGaming;
    public int enamyCounter;
    public float timer;
    public float timeTarget;
    public Player player;

    public HealthLine healthLine;

    public Level level;
    public Ammo ammo;

    private GameObject PausePanel;

    private GameObject EndPanel;
    public GameObject ShortRange;
    public GameObject LongRange;
    public GameObject Boomer;

    public AudioClip backgroundMusic;
    public AudioClip winMusic;
    public AudioClip loseNusic;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Awake()
    {
        StartTime = Time.time;
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
        audioSource = GetComponent<AudioSource>();
        isVictory = false;
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();
        isVictory = false;
        EndPanel = GameObject.Find("Canvas/EndPanel");
        EndPanel.SetActive(false);
    }
    
    private void GenerateEnamy()
    {
        float a = Random.Range(0f, 6.28f);
        int mod = Random.Range(0,100);
        Vector3 position  = new Vector3(Mathf.Cos(a),Mathf.Sin(a),0);
        if(mod>=0 && mod<=50)
        {
            Debug.Log("近战");
            GameObject p = Instantiate(ShortRange,transform);
            float distance = Random.Range(3,4);
            p.transform.position = distance * position + player.transform.position;
            enamyCounter++;
        }
        else if(mod>50 && mod<=75)
        {
            Debug.Log("远程");
            GameObject p = Instantiate(LongRange,transform);
            float distance = Random.Range(4,5);
            p.transform.position = distance * position + player.transform.position;
            enamyCounter++;
        }
        else
        {
            Debug.Log("自爆");
            GameObject p = Instantiate(Boomer,transform);
            p.transform.position = 5f * position + player.transform.position;
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
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Return()
    {
        Time.timeScale = 1f;
        StartCoroutine(Load());
    }

    public void Victory()
    {
        isVictory = true;
        audioSource.clip = winMusic;
        audioSource.loop = false;
        audioSource.Play();
        Time.timeScale = 0f;
    }

    public void Lose()
    {
        isVictory = true;
        audioSource.clip = loseNusic;
        EndPanel.SetActive(true);
        audioSource.loop = false;
        audioSource.Play();
        Time.timeScale = 0f;
    }

    
    // Update is called once per frame
    void Update()
    {
        if(Time.time - StartTime > timeTarget && !isVictory)
        {
            Debug.Log("胜利");
            Victory();
            EndPanel.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Escape) && isGaming && !isVictory)
        {
            Pause();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && !isGaming && !isVictory)
        {
            Restart();
        }

        if(timer == 0 && enamyCounter < 10)
        {
            GenerateEnamy();
        }

        timer += Time.deltaTime;
        if(timer >= 1.5f)
        {
            timer = 0f;
        }

        if(Input.GetKeyDown(KeyCode.F1))
        {
            Victory();
        }
        if(Input.GetKeyDown(KeyCode.F2))
        {
            Lose();
        }
        if(Input.GetKeyDown(KeyCode.F3))
        {
            player.InhanceExp(10);
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

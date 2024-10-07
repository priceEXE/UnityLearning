using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public float StartTime;
    private bool isEnd;
    private bool isGaming;
    public int enamyCounter;

    public int treeConter;
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

    public GameObject Tree;
    public AudioClip backgroundMusic;
    public AudioClip winMusic;
    public AudioClip loseNusic;

    public AudioClip clickButton;

    private AudioSource audioSource;

    private Text GameResult;
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
        isEnd = false;
        audioSource.clip = backgroundMusic;
        audioSource.loop = true;
        audioSource.Play();
        EndPanel = GameObject.Find("Canvas/EndPanel");
        GameResult = GameObject.Find("Canvas/EndPanel/Title").GetComponent<Text>();
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

    private void GenerateTree()
    {
        float a = Random.Range(0f, 6.28f);
        Vector3 position  = new Vector3(Mathf.Cos(a),Mathf.Sin(a),0);
        GameObject p = Instantiate(Tree,transform);
        float distance = 6f;
        p.transform.position = distance * position + player.transform.position;
        treeConter++;
    }
    public void decreaseEnamy()
    {
        enamyCounter--;
    }
    public void QuitGame()
    {
        audioSource.PlayOneShot(clickButton);
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
        audioSource.PlayOneShot(clickButton);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Return()
    {
        Time.timeScale = 1f;
        audioSource.PlayOneShot(clickButton);
        StartCoroutine(Load());
    }

    public void Victory()
    {
        GameResult.text = "You Win!";
        EndPanel.SetActive(true);
        isEnd = true;
        audioSource.clip = winMusic;
        audioSource.loop = false;
        audioSource.Play();
        Time.timeScale = 0f;
    }

    public void Lose()
    {
        GameResult.text = "You Lose!";
        isEnd = true;
        audioSource.clip = loseNusic;
        EndPanel.SetActive(true);
        audioSource.loop = false;
        audioSource.Play();
        Time.timeScale = 0f;
    }

    
    // Update is called once per frame
    void Update()
    {
        if(Time.time - StartTime > timeTarget && !isEnd)
        {
            Debug.Log("胜利");
            Victory();
            
        }

        if(Input.GetKeyDown(KeyCode.Escape) && isGaming && !isEnd)
        {
            Pause();
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && !isGaming && !isEnd)
        {
            Restart();
        }

        if(timer == 0 && enamyCounter < 10)
        {
            GenerateEnamy();
        }

        if( treeConter < 3)
        {
            GenerateTree();
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

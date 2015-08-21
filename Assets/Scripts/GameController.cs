using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public GameObject hazard;
    public Vector3 spawnValues;

    public float hazardCount;
    public float spawnWait;
    public float hazardWait;
    public float hazardSpeed;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;
    public int pointsForNextLevel;
    private string nextLevelName;
    
    private int score;
    private bool gameOver;
    private bool restart;
    private bool showAnimation = true;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(this.scoreText);
        DontDestroyOnLoad(this.restartText);
        DontDestroyOnLoad(this.gameOverText);
    }

    void Start()
    {
        this.scoreText = GameObject.Find("Score Text").GetComponent<GUIText>();
        this.restartText = GameObject.Find("Restart Text").GetComponent<GUIText>();
        this.gameOverText = GameObject.Find("Game Over Text").GetComponent<GUIText>();

        
        

        StartCoroutine(SpawnWaves());
        score = 0;
        this.UpdateScore();
        this.gameOver = false;
        this.restart = false;
        this.restartText.text = "";
        this.gameOverText.text = "";        
    }

    void Update()
    {        
        if(restart)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                Application.LoadLevel(Application.loadedLevel);
                Debug.Log("Level: "+Application.loadedLevelName);
            }            
        }
        Debug.Log("Level:" + Application.loadedLevelName);
        if (this.GetScore() >= pointsForNextLevel)
        {
            LoadNewScene();
        }
    }

    void LoadNewScene() {
        this.nextLevelName = GameObject.Find("Parameters").GetComponent<Parameters>().nextLevelName;

        Debug.Log(nextLevelName);

        this.restartText.text = "Proxima Fase";

        GameObject playerObject = GameObject.FindWithTag("Player");
        Rigidbody rigid = playerObject.GetComponent<Rigidbody>();
        
        Debug.Log(rigid.isKinematic);

        Transform playerTransform = playerObject.transform;
        playerTransform.position.Set(0, 3.0f, 0);

        playerObject.transform.parent = playerTransform;

        Debug.Log("Position:"+playerObject.transform.position.y);
        rigid.isKinematic = true;

        Animation winLevelAnimation = playerObject.GetComponent<Animation>();
        winLevelAnimation.Play("winlevel", PlayMode.StopAll);

        StartCoroutine(LoadAfterAnim(winLevelAnimation));
        
    }

    public IEnumerator LoadAfterAnim(Animation animation)
    {
        this.score = 0;
        this.pointsForNextLevel = 40;
        this.UpdateScore();        
        yield return new WaitForSeconds(animation["winlevel"].clip.length);
        Application.LoadLevelAsync(this.nextLevelName);
    }


    IEnumerator SpawnWaves()
    {
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;

                Mover mover = hazard.GetComponent<Mover>();
                mover.speed = hazardSpeed;

                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(hazardWait);
            }
            yield return new WaitForSeconds(spawnWait);

            if(gameOver)
            {
                this.restartText.text = "Aperte 'R' para reiniciar";
                this.restart = true;
                break;
            }
        }        
    }
    public void AddScore(int scoreValue)
    {
        this.score += scoreValue;
        this.UpdateScore();
    }

    void UpdateScore()
    {
        this.scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        this.gameOverText.text = "Game Over";
        this.gameOver = true;
    }

    public int GetScore()
    {
        return this.score;
    }

    public void SetRestart(bool value)
    {
        this.restart = value;
    }
}

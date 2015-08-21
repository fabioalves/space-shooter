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
    private Rigidbody playerRigidbody;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(this.scoreText);
        DontDestroyOnLoad(this.restartText);
        DontDestroyOnLoad(this.gameOverText);
        DontDestroyOnLoad(this.playerRigidbody);
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
                this.score = 0;
                this.UpdateScore();
                this.restartText.text = "";
                this.gameOver = false;
                this.gameOverText.text = "";
                Application.LoadLevel(Application.loadedLevel);
                StartCoroutine(this.SpawnWaves());
            }            
        }
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
        this.playerRigidbody = playerObject.GetComponent<Rigidbody>();

        Transform playerTransform = playerObject.transform;
        playerTransform.position.Set(0, 3.0f, 0);

        playerObject.transform.parent = playerTransform;
        
        this.playerRigidbody.isKinematic = true;

        Animation winLevelAnimation = playerObject.GetComponent<Animation>();
        winLevelAnimation.Play("winlevel", PlayMode.StopAll);


        if (!Application.isLoadingLevel)
        {
            this.score = 0;
            StartCoroutine(LoadAfterAnim(winLevelAnimation["winlevel"].clip.length));           
            
        }
    }

    public IEnumerator LoadAfterAnim(float animationSeconds)
    {
        
        yield return new WaitForSeconds(2.4f);
        
        this.UpdateScore();
        this.restartText.text = "";

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

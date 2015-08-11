using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public GameObject hazard;
    public Vector3 spawnValues;

    public float hazardCount;
    public float spawnWait;
    public float hazardWait;

    public GUIText scoreText;
    public GUIText restartText;
    public GUIText gameOverText;
    

    private int score;
    private bool gameOver;
    private bool restart;
    

    void Start()
    {
        Cursor.visible = false;
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
    }

    IEnumerator SpawnWaves()
    {
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y, spawnValues.z);
                Quaternion spawnRotation = Quaternion.identity;
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
}

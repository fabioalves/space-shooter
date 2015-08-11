using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public GameObject hazard;
    public Vector3 spawnValues;
    public float hazardCount;
    public float spawnWait;
    public float hazardWait;
    public GUIText scoreText;
    private int score;    

    void Start()
    {
        Cursor.visible = false;
        StartCoroutine(SpawnWaves());
        score = 0;
        this.UpdateScore();
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
        }        
    }
    public void AddScore(int scoreValue)
    {
        this.score += scoreValue;
        this.UpdateScore();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }
}

using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {
    private GameController gameController;
    private bool loadLevel = false;
    public string nextLevelName;
    public int nextLevelNeededPoints;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            this.gameController = gameControllerObject.GetComponent<GameController>();
        }

        if (this.gameController == null)
        {
            Debug.Log("Não encontrado o script GameController");
        }
    }


    void Update()
    {
        if (this.gameController.GetScore() >= nextLevelNeededPoints)
        {
            StartCoroutine(LoadNewScene());
        }
        if(loadLevel == true)
        {
            Application.LoadLevel(nextLevelName);
        }
    }

    IEnumerator LoadNewScene()
    {
        this.gameController.restartText.text = "Proxima Fase";
        loadLevel = true;
        yield return new WaitForSeconds(2);

    }
}

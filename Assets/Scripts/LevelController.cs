using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {
    private GameController gameController;
    private bool loadLevel = false;
    public string nextLevelName;
    public int nextLevelNeededPoints;

    public Animation winLevelAnimation;

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

        if (Input.GetKeyDown(KeyCode.U)) {
            Debug.Log("Animação");
            
        }

    }

    IEnumerator LoadNewScene()
    {
        this.gameController.restartText.text = "Proxima Fase";

        GameObject playerObject = GameObject.FindWithTag("Player");
        PlayerController pcontroller = playerObject.GetComponent<PlayerController>();
        pcontroller.YPosition(3.0f);
        winLevelAnimation = playerObject.GetComponent<Animation>();
        winLevelAnimation.Play("winlevel", PlayMode.StopAll);

        
        yield return new WaitForSeconds(2);

        this.loadLevel = true;
    }


}

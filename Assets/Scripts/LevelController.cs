using UnityEngine;
using System.Collections;

public class LevelController : MonoBehaviour {
    //private GameController gameController;
    //private GameObject playerObject;
    //private PlayerController pController;
    //private bool loadLevel = false;
    //private Animation winLevelAnimation;

    //public string nextLevelName;
    //public int pointsForNextLevel;    

    //void Start()
    //{
    //    GameObject gameControllerObject = GameObject.FindWithTag("GameController");
    //    if (gameControllerObject != null)
    //    {
    //        this.gameController = gameControllerObject.GetComponent<GameController>();
    //    }

    //    if (this.gameController == null)
    //    {
    //        Debug.Log("Não encontrado o script GameController");
    //    }
    //}

    //void Update()
    //{
    //    if (this.gameController == null)
    //    {
    //        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
    //        this.gameController = gameControllerObject.GetComponent<GameController>();
    //    }

    //    if (this.gameController.GetScore() >= pointsForNextLevel)
    //    {
    //        this.playerObject = GameObject.FindWithTag("Player");
    //        Debug.Log(playerObject);
    //        this.pController = playerObject.GetComponent<PlayerController>();
    //        this.LoadNewScene(playerObject, pController);
    //    }            
    //}

    //void LoadNewScene(GameObject player, PlayerController controller)
    //{        
    //    this.gameController.restartText.text = "Proxima Fase";

    //    controller.YPosition(3.0f);

    //    Debug.Log("Não encontrado: "+ controller.YPosition());

    //    winLevelAnimation = player.GetComponent<Animation>();
    //    winLevelAnimation.Play("winlevel", PlayMode.StopAll);

    //    if (!this.winLevelAnimation.isPlaying)
    //    {
    //        Debug.Log("Terminado");
    //        Application.LoadLevel(nextLevelName);
    //    }
    //}


}

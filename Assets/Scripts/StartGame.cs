using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	public void LoadScene(string level)
    {
        Application.LoadLevel(level);
    }
}

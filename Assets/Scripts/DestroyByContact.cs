﻿using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;
    public int scoreValue;
    private GameController gameController;

    void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");

        if (gameControllerObject == null)
        {
            Debug.Log("Não encontrado o script gameControllerObject");
        }

        if (gameControllerObject != null)
        {
            this.gameController = gameControllerObject.GetComponent<GameController>();
        }

        if(this.gameController == null)
        {
            Debug.Log("Não encontrado o script GameController");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Boundary"))
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
            Instantiate(explosion, transform.position, transform.rotation);

            if (other.tag.Equals("Bolt"))
            {
                if (this.gameController == null)
                {
                    Debug.Log("Não encontrado o script GameController");
                }
                this.gameController.AddScore(scoreValue);
            }
            if (other.tag.Equals("Player"))
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
                this.gameController.GameOver();
            }            
        }
    }
}

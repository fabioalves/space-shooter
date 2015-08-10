using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

    public GameObject explosion;
    public GameObject playerExplosion;

    void OnTriggerEnter(Collider other)
    {
        if (!other.tag.Equals("Boundary"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
            Instantiate(explosion, transform.position, transform.rotation);
            if (other.tag.Equals("Player"))
            {
                Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
            }
        }
    }
}

using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {
    Rigidbody rbody;
    public float speed;
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        rbody.velocity = transform.forward * speed;
    }
}

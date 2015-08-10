using UnityEngine;
using System.Collections;

public class RandomRotator : MonoBehaviour {
    public float tumble;
    Rigidbody rbody;
    void Start()
    {
        rbody = GetComponent<Rigidbody>();

        rbody.angularVelocity = Random.insideUnitSphere * tumble;
    }
}

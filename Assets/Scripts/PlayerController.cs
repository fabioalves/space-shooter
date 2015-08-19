﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public Boundary boundary;
    public float tilt;
    public float frontTilt;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;
    private AudioSource audioSource;

    private float yPosition = 0.0f;

    public void YPosition(float position)
    {
        this.yPosition = position;
    }

    Rigidbody rbody;
    void Start()
    {
        rbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audioSource.Play();
        }
    }


    void FixedUpdate()
    {
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(moveHorizontal, yPosition, moveVertical);
        rbody.velocity = movement * speed;

        rbody.position = new Vector3(
            Mathf.Clamp(rbody.position.x, boundary.xMin, boundary.xMax),
            yPosition,
            Mathf.Clamp(rbody.position.z, boundary.zMin, boundary.zMax)
            );

        rbody.rotation = Quaternion.Euler(rbody.velocity.z * frontTilt, yPosition, rbody.velocity.x * -tilt);
        
    }
}

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

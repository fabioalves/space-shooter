using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 8;
    public Boundary boundary = new Boundary()
    {
        xMin = -7.7f,
        xMax = 7.7f,
        zMin = -4,
        zMax = 8
    };
    public float tilt = 4;
    public float frontTilt = 2;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;
    private AudioSource audioSource;
       

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
        Vector3 movement = new Vector3(moveHorizontal, rbody.position.y, moveVertical);
        rbody.velocity = movement * speed;

        rbody.position = new Vector3(
            Mathf.Clamp(rbody.position.x, boundary.xMin, boundary.xMax),
            rbody.position.y,
            Mathf.Clamp(rbody.position.z, boundary.zMin, boundary.zMax)
            );

        rbody.rotation = Quaternion.Euler(rbody.velocity.z * frontTilt, rbody.position.y, rbody.velocity.x * -tilt);
        
    }

}

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

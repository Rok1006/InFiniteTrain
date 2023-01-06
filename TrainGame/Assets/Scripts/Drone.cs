using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public GameObject player;
    public float speed = 0.1f;
    public float verticalSpeed = 0.1f;
    private float maxSpeed = 300f;
    public float acceleration = 10f;
    public float startRotating = 0;
    private Rigidbody rb;
    public Transform movePoint;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(player.transform);
        //transform.Translate(Vector3.forward * speed, Space.World);
        var step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, movePoint.position, step);


    }

    public void Rotate()
    {
        if (speed < -startRotating)
        {
            transform.Rotate(0, 0, 30);
        }
    }
}
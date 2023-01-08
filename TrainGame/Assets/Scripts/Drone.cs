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
    public Vector3 movePoint;
    public GameObject thisTrain;
    public bool isMoving = false;
    public bool arrived = false;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        
        rb = this.GetComponent<Rigidbody>();
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 100);
        for(int i = 0; i < hitColliders.Length; i++)
        {
            Debug.Log(hitColliders[i].name);
            if(hitColliders[i].CompareTag("center"))
            {
                thisTrain = hitColliders[i].gameObject;
            }
            
        
        }
        movePoint = PickSpotToMove();
    }

    // Update is called once per frame
    void Update()
    {

        transform.LookAt(player.transform);
        //transform.Translate(Vector3.forward * speed, Space.World);
        var step = speed * Time.deltaTime;
        transform.position = Vector3.Lerp(transform.position, movePoint, step);
      


    }

    public void Rotate()
    {
        if (speed < -startRotating)
        {
            transform.Rotate(0, 0, 30);
        }
    }

    public Vector3 PickSpotToMove()
    {
        var xOffset = Random.Range(-3, 3);
        var zOffset = Random.Range(-10, 10);
        movePoint = thisTrain.transform.position + new Vector3(xOffset, 0, zOffset);

        return movePoint;
    }
}
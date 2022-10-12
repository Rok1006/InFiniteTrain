using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMC : MonoBehaviour
{
    [Header("Assignment")]
    [SerializeField] private GameObject FrontMC;
    public float moveSpeed;
    public bool facingFront = true;   //or side
    [Header("Keys")]
    public KeyCode leftKey;
    public KeyCode rightKey;
    public KeyCode upKey;
    public KeyCode downKey;

//---------
    Animator frontAnim;
    private Rigidbody rb;
    private Vector2 moveInput;
    Camera myCam;
    float x;
    float y;
    float z;
    private float oldPositionX = 0.0f;
    private float oldPositionZ = 0.0f;
    
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        frontAnim = FrontMC.GetComponent<Animator>();
        myCam = Camera.main;
        x = this.transform.localScale.x;
        y = this.transform.localScale.y;
        z = this.transform.localScale.z;
        oldPositionX = transform.position.x;
        oldPositionZ = transform.position.z;

    }

    void FixedUpdate() {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
       // moveInput = Vector2.zero;
        // if(Input.GetKeyDown(KeyCode.A)){
        //     moveInput += Vector2.left;
        // }else if(Input.GetKeyDown(KeyCode.D)){
        //     moveInput += Vector2.right;
        // }else if(Input.GetKeyDown(KeyCode.W)){
        //     moveInput += Vector2.up;
        // }else if(Input.GetKeyDown(KeyCode.S)){
        //     moveInput += Vector2.down;
        // }else{moveInput = Vector2.zero;}

        moveInput.Normalize();
        rb.velocity = new Vector3(-moveInput.x*moveSpeed, rb.velocity.y, -moveInput.y*moveSpeed); //rb.velocity.y
    }

    void Update()
    {

        if (transform.position.x > oldPositionX) //Rotate the anim object instead of main
        {
            FrontMC.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (transform.position.x < oldPositionX)
        {
            FrontMC.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        
//Mpvenment--------
        if(transform.position.z > oldPositionZ || transform.position.z < oldPositionZ) //up and down
        {
            facingFront = true;
            DisableAllAnim();
            frontAnim.SetBool("front_walk", true);
            frontAnim.SetBool("front_idle", false);  
            Debug.Log(oldPositionX + "-"+ transform.position.x);
        }else if(transform.position.x > oldPositionX || transform.position.x < oldPositionX){ //left and right
            facingFront = false;
            DisableAllAnim();
            frontAnim.SetBool("side_walk", true);
            frontAnim.SetBool("side_idle", false); 
        }  
        if(moveInput.x == 0 && moveInput.y == 0){ //player stop
            if(facingFront){
                DisableAllAnim();
                frontAnim.SetBool("front_idle", true);  
            }else{
                DisableAllAnim();
                frontAnim.SetBool("side_idle", true);  
            }
        }else{
            frontAnim.SetBool("side_idle", false);  
            frontAnim.SetBool("front_idle", false); 
        }
        oldPositionX = transform.position.x;
        oldPositionZ = transform.position.z;
    }
    public void DisableAllAnim(){
        frontAnim.SetBool("front_walk", false);
        frontAnim.SetBool("front_idle", false);  
        frontAnim.SetBool("side_walk", false);
        frontAnim.SetBool("side_idle", false); 

    }
}

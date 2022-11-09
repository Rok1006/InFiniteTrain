using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Not using this script
public class PlayerMC : MonoBehaviour
{
    public enum PlayerState{NORMAL, USINGGUN, USINGSWORD};
    public PlayerState currentState = PlayerState.NORMAL;
    private int directionFacing = 0; //0 = front, 1 = back;

    [Header("Assignment")]
    [SerializeField] private GameObject FrontMC;
    [SerializeField] private GameObject BackMC;
    public float moveSpeed;
    public bool facingFront = true;   //or side

    // [Header("Keys")]
    // public KeyCode leftKey;
    // public KeyCode rightKey;
    // public KeyCode upKey;
    // public KeyCode downKey;
//---------
    Animator frontAnim;
    Animator backAnim;
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
        backAnim = BackMC.GetComponent<Animator>();
        myCam = Camera.main;
        x = this.transform.localScale.x;
        y = this.transform.localScale.y;
        z = this.transform.localScale.z;
        oldPositionX = transform.position.x;
        oldPositionZ = transform.position.z;

        FrontMC.SetActive(true);
        BackMC.SetActive(false);

    }

    void FixedUpdate() {
        moveInput.x = Input.GetAxisRaw("Horizontal"); //set up change key at runtime
        moveInput.y = Input.GetAxisRaw("Vertical");

        moveInput.Normalize();
        rb.velocity = new Vector3(-moveInput.x*moveSpeed, rb.velocity.y, -moveInput.y*moveSpeed); //rb.velocity.y
    }

    void Update()
    {
        if (transform.position.x > oldPositionX) //Rotate the anim object instead of main
        {
            FrontMC.transform.eulerAngles = new Vector3(0, 0, 0);
            BackMC.transform.eulerAngles = new Vector3(0, 0, 0);
        }else if (transform.position.x < oldPositionX)
        {
            FrontMC.transform.eulerAngles = new Vector3(0, 180, 0);
            BackMC.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        if (transform.position.z > oldPositionZ) //Change player gameObject
        {
            FrontMC.SetActive(true);
            BackMC.SetActive(false);
        }else if (transform.position.z < oldPositionZ)
        {
            FrontMC.SetActive(false);
            BackMC.SetActive(true);
        }
//Movenment--------
        switch(directionFacing){  //wrap it with a larger switch case
            case 0:  //face front
                MCBasicAction();
            break;
            case 1:  //face back

            break;
        }
        oldPositionX = transform.position.x;
        oldPositionZ = transform.position.z;
    }
    public void DisableAllAnim(){
        frontAnim.SetBool("front_walk", false);
        frontAnim.SetBool("front_idle", false);  
        frontAnim.SetBool("side_walk", false);
        frontAnim.SetBool("side_idle", false); 
        backAnim.SetBool("back_walk", false);
        backAnim.SetBool("back_idle", false);  
        backAnim.SetBool("side_walk", false);
        backAnim.SetBool("side_idle", false); 

    }
    public void MCBasicAction(){
        if(transform.position.z > oldPositionZ || transform.position.z < oldPositionZ) //up and down
        {
            facingFront = true;
            DisableAllAnim();
            frontAnim.SetBool("front_walk", true);
            frontAnim.SetBool("front_idle", false);  
            backAnim.SetBool("back_walk", true);
            backAnim.SetBool("back_idle", false); 
            // Debug.Log(oldPositionX + "-"+ transform.position.x);
        }else if(transform.position.x > oldPositionX || transform.position.x < oldPositionX){ //left and right
            facingFront = false;
            DisableAllAnim();
            frontAnim.SetBool("side_walk", true);
            frontAnim.SetBool("side_idle", false); 
            backAnim.SetBool("side_walk", true);
            backAnim.SetBool("side_idle", false); 
        }  
        if(moveInput.x == 0 && moveInput.y == 0){ //player stop
            if(facingFront){
                DisableAllAnim();
                frontAnim.SetBool("front_idle", true);  
                backAnim.SetBool("back_idle", true);  
            }else{
                DisableAllAnim();
                frontAnim.SetBool("side_idle", true);  
                backAnim.SetBool("side_idle", true);  
            }
        }else{
            frontAnim.SetBool("side_idle", false);  
            frontAnim.SetBool("front_idle", false); 
            backAnim.SetBool("side_idle", false);  
            backAnim.SetBool("back_idle", false); 
        }
    }
    public void MCBackAnim(){

    }
}

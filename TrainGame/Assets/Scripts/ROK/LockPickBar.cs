using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;

public class LockPickBar : MonoBehaviour
{
    [SerializeField, BoxGroup("REF")] GameObject Top;  //the hadle bro
    [SerializeField, BoxGroup("REF")] Slider T_S;
    [SerializeField, BoxGroup("REF")] float TSpeed; //0.0001f
    [SerializeField, BoxGroup("REF")] GameObject Mid;
    [SerializeField, BoxGroup("REF")] Slider M_S;
    [SerializeField, BoxGroup("REF")] float MSpeed;
    [SerializeField, BoxGroup("REF")] GameObject Bottom;  //last layer layer 3
    [SerializeField, BoxGroup("REF")] Slider B_S;
    [SerializeField, BoxGroup("REF")] float BSpeed;
    [SerializeField, BoxGroup("REF")] GameObject PT;  //the handle
    [SerializeField, BoxGroup("REF")] GameObject PM;
    [SerializeField, BoxGroup("REF")] GameObject PB;

    public bool Complete;
    [SerializeField] List<GameObject> list;
    public int iterator;
    public GameObject current;
    [SerializeField] AudioSource unlockSound;
    // Animator anim;

    RectTransform t;
    RectTransform m;
    RectTransform b;
    RectTransform pt;
    RectTransform pm;
    RectTransform pb;

    float T_pos, M_pos, B_pos;
    float T_width, M_width, B_width;
    float ranT, ranM, ranB;
    bool canMoveT, canMoveM, canMoveB;

    float targetPosT;

    void Start()
    {
        t = Top.GetComponent<RectTransform>();
        b = Mid.GetComponent<RectTransform>();
        m = Bottom.GetComponent<RectTransform>();
        pt = PT.GetComponent<RectTransform>();
        pm = PM.GetComponent<RectTransform>();
        pb = PB.GetComponent<RectTransform>();

        iterator = 0;

        T_S.value = 0;
        M_S.value = 0;
        B_S.value = 0; 

        T_width = SetRandomBarWidth(70, 150);
        M_width = SetRandomBarWidth(50, 120);
        B_width = SetRandomBarWidth(10, 50);
        
        t.sizeDelta = new Vector2(T_width, 0);
        m.sizeDelta = new Vector2(M_width, 0);
        b.sizeDelta = new Vector2(B_width, 0);

        ranT = GetRanNum();
        ranM = GetRanNum();
        ranB = GetRanNum();
        //anim = this.GetComponent<Animator>();

    }


    void Update()
    {
        current = list[iterator];
        T_S.value = T_pos;
        M_S.value = M_pos;
        B_S.value = B_pos; 

        // targetPosT = 10;
        if(canMoveT){
            if(CompareHandleToRan(T_pos, ranT)){
                while(T_pos<ranT){
                    T_pos+=TSpeed; 
                    //Debug.Log("yoooooooooooooo");
                }
                canMoveT = false;
            }
            if(!CompareHandleToRan(T_pos, ranT)){
                while(T_pos>ranT){
                    T_pos-=TSpeed; 
                    //Debug.Log("yoooooooooooooo");
                }
                canMoveT = false;
            }
        }
        if(T_pos!=ranT){
            canMoveT = true;
            //
        }
        // if(T_pos==ranT){
        //     ranT = GetRanNum();
        //     Debug.Log("arrive");
        // }
        //else{
        //     canMoveT = true;
        // }
        // if(T_pos!=10){
            
        // }else if(T_pos>=10){
        //     T_pos-=TSpeed; 
        // }
        // if(M_pos<10){
        //     M_pos+=MSpeed; 
        // }else if(M_pos>=10){
        //     M_pos-=MSpeed; 
        // }
        // if(B_pos<10){
        //     B_pos+=BSpeed; 
        // }else if(B_pos>=10){
        //     B_pos-=BSpeed; 
        // }

        CheckLockBar();
    }
    bool CompareHandleToRan(float pos, float ran){  //increase = true
        bool isIncrease = false;
        if(pos<ran){
            isIncrease =  true;
        }else if(pos>ran){
            isIncrease =  false;
        }
        return isIncrease;
        
    }
    void CheckLockBar(){
        if (Input.GetKeyUp(KeyCode.L))
        {
            if (current == Top && pt.rect.Overlaps(t.rect)) //We have to check the certain angle range of values, collisions will mess UI up, and sorry its the simplest way for now :(
            {
                //anim.SetTrigger("pulse");
                list[iterator].gameObject.SetActive(false);
                //unlockSound.Play();
                iterator++;
                Debug.Log("layer1");
            } else if (current == Mid && pm.rect.Overlaps(m.rect)) //We have to check the certain angle range of values, collisions will mess UI up, and sorry its the simplest way for now :(
            {
                //anim.SetTrigger("pulse");
                list[iterator].gameObject.SetActive(false);
                //unlockSound.Play(); 
                iterator++;
                Debug.Log("layer2");
            }
            else if (current == Bottom && pb.rect.Overlaps(b.rect)) //We have to check the certain angle range of values, collisions will mess UI up, and sorry its the simplest way for now :(
            {
                //anim.SetTrigger("pulse");
                //unlockSound.Play();
                list[iterator].gameObject.SetActive(false);
                Complete = true;
                Debug.Log("layer3");
            }else{
                Debug.Log("Bruh");
            }
        }
    }
    float SetRandomBarWidth(int min, int max){ //min 10 max 150
        float ranWidth = Random.Range(min,max);
        return ranWidth;
    }

    float GetRanNum(){
        int ranN = Random.Range(0,10);
        return ranN;
    }
}

//it always go frm left right right left

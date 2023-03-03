using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using Random=UnityEngine.Random;

public class LockPickGame : MonoBehaviour
{
    [SerializeField] GameObject Inner;
    [SerializeField] GameObject Middle;
    [SerializeField] GameObject Outer;
    [SerializeField] GameObject Hand;  //the handle
    [SerializeField] Vector2 InnerAngles; //vector should be organized by smaller then larger angle
    [SerializeField] Vector2 MiddleAngles;
    [SerializeField] Vector2 OuterAngles;

    public bool Complete;
    [SerializeField] List<GameObject> list;
    public int iterator;
    public GameObject current;
    [SerializeField] AudioSource unlockSound;

    [SerializeField] float areaAmt_Outer;
    [SerializeField] float areaAmt_Middle;
    [SerializeField] float areaAmt_Inner;

    RectTransform outer;
    RectTransform middle;
    RectTransform inner;
    RectTransform hand;
    Animator anim;

    void Start()
    {
        SetRangeForRings(); //Set Random Range at start
        outer = Outer.GetComponent<RectTransform>();
        middle = Middle.GetComponent<RectTransform>();
        inner = Inner.GetComponent<RectTransform>();
        hand = Hand.GetComponent<RectTransform>();

        anim = this.GetComponent<Animator>();
        

        for(int i = 0; i < list.Count; i++)
        {
            Outer.GetComponent<Image>().enabled = true;
            //list[i].gameObject.GetComponent<Image>().color= new Color(0.2f,0.2f,0.2f,1f);
        }
        iterator = 0;
    }

    void Update()
    {
        current = list[iterator];
        //list[iterator].gameObject.GetComponent<Image>().color = Color.white;
        //Debug.Log("bruh:" + Middle.GetComponent<RectTransform>().eulerAngles.z);
        CheckLock();
        
    }

    void CheckLock_EU(){
        if (Input.GetKeyUp(KeyCode.Space))
        {

            if (current == Outer && Outer.GetComponent<RectTransform>().eulerAngles.z >= OuterAngles.x && Outer.GetComponent<RectTransform>().eulerAngles.z <= OuterAngles.y) //We have to check the certain angle range of values, collisions will mess UI up, and sorry its the simplest way for now :(
            {
                //list[iterator].gameObject.GetComponent<Image>().color = Color.black;
                // anim.SetTrigger("pulse");
                unlockSound.Play();
                iterator++;
            } else if (current == Middle && Middle.GetComponent<RectTransform>().eulerAngles.z >= MiddleAngles.x && Middle.GetComponent<RectTransform>().eulerAngles.z <= MiddleAngles.y) //We have to check the certain angle range of values, collisions will mess UI up, and sorry its the simplest way for now :(
            {
                //list[iterator].gameObject.GetComponent<Image>().color = Color.black;
                // anim.SetTrigger("pulse");
                unlockSound.Play(); 
                iterator++;
            }
            else if (current == Inner && Inner.GetComponent<RectTransform>().eulerAngles.z >= InnerAngles.x && Inner.GetComponent<RectTransform>().eulerAngles.z <= InnerAngles.y) //We have to check the certain angle range of values, collisions will mess UI up, and sorry its the simplest way for now :(
            {
                // anim.SetTrigger("pulse");
                unlockSound.Play();
                //list[iterator].gameObject.GetComponent<Image>().color = Color.black;
                Complete = true;
            }
            else
            {
                //no more wheels
            }
        }
    }

    void CheckLock(){ //by rect transform

        if (Input.GetKeyUp(KeyCode.L))
        {
            if (current == Outer && hand.rect.Overlaps(outer.rect)) //We have to check the certain angle range of values, collisions will mess UI up, and sorry its the simplest way for now :(
            {
                anim.SetTrigger("pulse");
                list[iterator].gameObject.GetComponent<Image>().enabled = false;
                unlockSound.Play();
                iterator++;
                Debug.Log("layer1");
            } else if (current == Middle && hand.rect.Overlaps(middle.rect)) //We have to check the certain angle range of values, collisions will mess UI up, and sorry its the simplest way for now :(
            {
                anim.SetTrigger("pulse");
                list[iterator].gameObject.GetComponent<Image>().enabled = false;
                unlockSound.Play(); 
                iterator++;
                Debug.Log("layer2");
            }
            else if (current == Inner && hand.rect.Overlaps(inner.rect)) //We have to check the certain angle range of values, collisions will mess UI up, and sorry its the simplest way for now :(
            {
                anim.SetTrigger("pulse");
                unlockSound.Play();
                list[iterator].gameObject.GetComponent<Image>().enabled = false;
                Complete = true;
                Debug.Log("layer3");
            }else{
                Debug.Log("Bruh");
            }
        }
    }

    void Lock_Bar(){
        
    }

    void SetRangeForRings(){
        float ran_Outer = Random.Range(0.581f, 1f);
        float ran_Middle = Random.Range(0.3f, 0.658f);
        float ran_Inner = Random.Range(0.1f, 0.4f);

        Outer.GetComponent<Image>().fillAmount = ran_Outer;
        Middle.GetComponent<Image>().fillAmount = ran_Middle;
        Inner.GetComponent<Image>().fillAmount = ran_Inner;
    }
}

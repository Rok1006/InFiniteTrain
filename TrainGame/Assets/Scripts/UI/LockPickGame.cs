using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

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

    private float areaAmt_Outer;
    private float areaAmt_Middle;
    private float areaAmt_Inner;

    void Start()
    {
        

        for(int i = 0; i < list.Count; i++)
        {
            //list[i].gameObject.GetComponent<Image>().color= new Color(0.2f,0.2f,0.2f,1f);
        }
        iterator = 0;
    }

    // Update is called once per frame
    void Update()
    {
        current = list[iterator];
        //list[iterator].gameObject.GetComponent<Image>().color = Color.white;
        Debug.Log("bruh:" + Middle.GetComponent<RectTransform>().eulerAngles.z);
        if (Input.GetKeyUp(KeyCode.Space))
        {

            if (current == Outer && Outer.GetComponent<RectTransform>().eulerAngles.z >= OuterAngles.x && Outer.GetComponent<RectTransform>().eulerAngles.z <= OuterAngles.y) //We have to check the certain angle range of values, collisions will mess UI up, and sorry its the simplest way for now :(
            {
                //list[iterator].gameObject.GetComponent<Image>().color = Color.black;
                unlockSound.Play();
                iterator++;
            } else if (current == Middle && Middle.GetComponent<RectTransform>().eulerAngles.z >= MiddleAngles.x && Middle.GetComponent<RectTransform>().eulerAngles.z <= MiddleAngles.y) //We have to check the certain angle range of values, collisions will mess UI up, and sorry its the simplest way for now :(
            {
                //list[iterator].gameObject.GetComponent<Image>().color = Color.black;
                unlockSound.Play(); 
                iterator++;
            }
            else if (current == Inner && Inner.GetComponent<RectTransform>().eulerAngles.z >= InnerAngles.x && Inner.GetComponent<RectTransform>().eulerAngles.z <= InnerAngles.y) //We have to check the certain angle range of values, collisions will mess UI up, and sorry its the simplest way for now :(
            {
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
    void CheckLock(){

    }
}

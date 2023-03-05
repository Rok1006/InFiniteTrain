using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LockPickBar : MonoBehaviour
{
    [SerializeField, BoxGroup("REF")] GameObject Top;  //the hadle bro
    //[SerializeField, BoxGroup("REF")] Slider T_S;
    [SerializeField, BoxGroup("REF")] float TSpeed; //0.0001f
    [SerializeField, BoxGroup("REF")] GameObject Mid;
    //[SerializeField, BoxGroup("REF")] Slider M_S;
    [SerializeField, BoxGroup("REF")] float MSpeed;
    [SerializeField, BoxGroup("REF")] GameObject Bottom;  //last layer layer 3
    //[SerializeField, BoxGroup("REF")] Slider B_S;
    [SerializeField, BoxGroup("REF")] float BSpeed;
    [SerializeField, BoxGroup("REF")] GameObject PT;  //the handle
    [SerializeField, BoxGroup("REF")] GameObject PM;
    [SerializeField, BoxGroup("REF")] GameObject PB;

    // public GameObject test1;
    //  public GameObject test2;
    // RectTransform t1;
    // RectTransform t2;

    public bool Complete;
    [SerializeField] List<GameObject> list;
    public int iterator;
    [ReadOnly]public GameObject current;
    [SerializeField] AudioSource unlockSound;
    Animator anim;

    RectTransform t;
    RectTransform m;
    RectTransform b;
    RectTransform pt;
    RectTransform pm;
    RectTransform pb;

    float T_pos, M_pos, B_pos;
    float T_width, M_width, B_width;
    //public float moveSpeedT, moveSpeedM, moveSpeedB;
    bool isMovingT, isMovingM, isMovingB = false;

    float targetPosT;
    private Vector2 touchMargin;// Flag to indicate whether the static UI is touching/overlapping with the moving UI
    private bool isTouching = false;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        t = Top.GetComponent<RectTransform>();
        m = Mid.GetComponent<RectTransform>();
        b = Bottom.GetComponent<RectTransform>();
        pt = PT.GetComponent<RectTransform>();
        pm = PM.GetComponent<RectTransform>();
        pb = PB.GetComponent<RectTransform>();

        iterator = 0;

        // T_S.value = 0;
        // M_S.value = 0;
        // B_S.value = 0; 

        t.sizeDelta = new Vector2(SetRandomBarWidth(150, 200), t.sizeDelta.y);
        m.sizeDelta = new Vector2(SetRandomBarWidth(100, 150), m.sizeDelta.y);
        b.sizeDelta = new Vector2(SetRandomBarWidth(30, 120), b.sizeDelta.y);

        t.anchoredPosition = new Vector2(Random.Range(-151, 151), t.anchoredPosition.y);
        m.anchoredPosition = new Vector2(Random.Range(-151, 151), m.anchoredPosition.y);
        b.anchoredPosition = new Vector2(Random.Range(-151, 151), b.anchoredPosition.y);
        // //t.rect.width = SetRandomBarWidth(70, 150);
        // m.rect.width = SetRandomBarWidth(50, 120);
        // b.rect.width = SetRandomBarWidth(10, 70);
        
        // t.sizeDelta = new Vector2(T_width, 0);
        // m.sizeDelta = new Vector2(M_width, 0);
        // b.sizeDelta = new Vector2(B_width, 0);
        // T_S.value = GetRanNum();  //set initial spot
        //  M_S.value = GetRanNum();
        //   B_S.value = GetRanNum();

        StartCoroutine(MoveTop());
        StartCoroutine(MoveMiddle());
        StartCoroutine(MoveBottom());

        // t2 = test2.GetComponent<RectTransform>();
        // t1 = test1.GetComponent<RectTransform>();
    }

    void Update()
    {
        current = list[iterator];
        CheckLockBar();

        if(CheckOverlap(t, pt)){ //if current
            Debug.Log("heyyyyyyy");
        }
        
    }
               
    public bool IsTouching()
    {
        return isTouching;
    }

    IEnumerator MoveTop()  //-151, 151
    {
        float targetValue = Random.Range(-151, 151);// Generate a random value between minRange and maxRange
        Vector2 targetPosition = new Vector2(targetValue, t.anchoredPosition.y);
        isMovingT = true;// Move the handle towards the target value
        while (t.anchoredPosition.x != targetValue)
        {
            //t.anchoredPosition.x = Mathf.MoveTowards(t.anchoredPosition.x, targetValue, TSpeed * Time.deltaTime);
            t.anchoredPosition = Vector2.MoveTowards(t.anchoredPosition, targetPosition, TSpeed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("done");
        isMovingT = false;
        // Wait until the handle arrives at the target value before generating a new random value
        yield return new WaitUntil(() => !isMovingT);
        // Start the coroutine again to generate a new random value and move the handle towards it
        StartCoroutine(MoveTop());
    }
    IEnumerator MoveMiddle()
    {
        float targetValue = Random.Range(-151, 151);// Generate a random value between minRange and maxRange
        Vector2 targetPosition = new Vector2(targetValue, m.anchoredPosition.y);
        isMovingM = true;// Move the handle towards the target value
        while (m.anchoredPosition.x != targetValue)
        {
            //t.anchoredPosition.x = Mathf.MoveTowards(t.anchoredPosition.x, targetValue, TSpeed * Time.deltaTime);
            m.anchoredPosition = Vector2.MoveTowards(m.anchoredPosition, targetPosition, MSpeed * Time.deltaTime);
            yield return null;
        }
        isMovingM = false;
        // Wait until the handle arrives at the target value before generating a new random value
        yield return new WaitUntil(() => !isMovingM);
        // Start the coroutine again to generate a new random value and move the handle towards it
        StartCoroutine(MoveMiddle());
    }
    IEnumerator MoveBottom()
    {
        float targetValue = Random.Range(-151, 151);// Generate a random value between minRange and maxRange
        Vector2 targetPosition = new Vector2(targetValue, b.anchoredPosition.y);
        isMovingT = true;// Move the handle towards the target value
        while (b.anchoredPosition.x != targetValue)
        {
            //t.anchoredPosition.x = Mathf.MoveTowards(t.anchoredPosition.x, targetValue, TSpeed * Time.deltaTime);
            b.anchoredPosition = Vector2.MoveTowards(b.anchoredPosition, targetPosition, BSpeed * Time.deltaTime);
            yield return null;
        }
        isMovingB = false;
        // Wait until the handle arrives at the target value before generating a new random value
        yield return new WaitUntil(() => !isMovingB);
        // Start the coroutine again to generate a new random value and move the handle towards it
        StartCoroutine(MoveBottom());
    }

    void CheckLockBar(){
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (current == Top && CheckOverlap(t, pt)) //We have to check the certain angle range of values, collisions will mess UI up, and sorry its the simplest way for now :(
            {
                anim.SetTrigger("pulse");
                list[iterator].gameObject.SetActive(false);
                unlockSound.Play();
                iterator++;
                Debug.Log("layer1");
            } else if (current == Mid && CheckOverlap(m, pm)) //We have to check the certain angle range of values, collisions will mess UI up, and sorry its the simplest way for now :(
            {
                anim.SetTrigger("pulse");
                list[iterator].gameObject.SetActive(false);
                unlockSound.Play(); 
                iterator++;
                Debug.Log("layer2");
            }
            else if (current == Bottom && CheckOverlap(b, pb)) //We have to check the certain angle range of values, collisions will mess UI up, and sorry its the simplest way for now :(
            {
                anim.SetTrigger("pulse");
                unlockSound.Play();
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
    bool rectOverlaps(RectTransform rectTrans1, RectTransform rectTrans2) //this work on normal default ui, this work but the ui for the slider bar is somewhere else
    {
        Rect rect1 = new Rect(rectTrans1.anchoredPosition.x, rectTrans1.anchoredPosition.y, rectTrans1.rect.width, rectTrans1.rect.height);  //for some reason *2 makes the 
        Rect rect2 = new Rect(rectTrans2.anchoredPosition.x, rectTrans2.anchoredPosition.y, rectTrans2.rect.width, rectTrans2.rect.height);
        //Debug.Log(rect1);
        Debug.Log(rect2);
        return rect1.Overlaps(rect2);
    }//not usng

    bool CheckOverlap(RectTransform MUI, RectTransform SUI){
        Rect staticRect = new Rect(SUI.GetComponent<RectTransform>().anchoredPosition - SUI.GetComponent<RectTransform>().rect.size / 2, SUI.GetComponent<RectTransform>().rect.size);
        Rect movingRect = new Rect(MUI.anchoredPosition - MUI.rect.size / 2,MUI.rect.size);

        touchMargin.x = (MUI.rect.width + SUI.GetComponent<RectTransform>().rect.width) / 2;
        touchMargin.y = (MUI.rect.height + SUI.GetComponent<RectTransform>().rect.height) / 2;
        // Check if the two UI elements are touching/overlapping
        return staticRect.Overlaps(movingRect) && 
            Mathf.Abs(MUI.anchoredPosition.x - SUI.GetComponent<RectTransform>().anchoredPosition.x) < touchMargin.x &&
            Mathf.Abs(MUI.anchoredPosition.y - SUI.GetComponent<RectTransform>().anchoredPosition.y) < touchMargin.y;
        // {
        //     isTouching = true;
        //     Debug.Log("heyyyyyyy");
        // }
        // else
        // {
        //     isTouching = false;
        // }
    }


 
}

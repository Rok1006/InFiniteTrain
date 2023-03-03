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
    public float moveSpeedT, moveSpeedM, moveSpeedB;
    bool isMovingT, isMovingM, isMovingB = false;

    float targetPosT;

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

        T_S.value = 0;
        M_S.value = 0;
        B_S.value = 0; 

        T_width = SetRandomBarWidth(70, 150);
        M_width = SetRandomBarWidth(50, 120);
        B_width = SetRandomBarWidth(10, 70);
        
        t.sizeDelta = new Vector2(T_width, 0);
        m.sizeDelta = new Vector2(M_width, 0);
        b.sizeDelta = new Vector2(B_width, 0);
        T_S.value = GetRanNum();  //set initial spot
         M_S.value = GetRanNum();
          B_S.value = GetRanNum();

        StartCoroutine(MoveTop());
        StartCoroutine(MoveMiddle());
        StartCoroutine(MoveBottom());
    }

    void Update()
    {
        current = list[iterator];
        CheckLockBar();
        if(pt.rect.Overlaps(t.rect)){
            Debug.Log("heyyyyyyy");
        }
    }

    IEnumerator MoveTop()
    {
        float targetValue = Random.Range(0, 10);// Generate a random value between minRange and maxRange
        isMovingT = true;// Move the handle towards the target value
        while (T_S.value != targetValue)
        {
            T_S.value = Mathf.MoveTowards(T_S.value, targetValue, TSpeed * Time.deltaTime);
            yield return null;
        }
        isMovingT = false;
        // Wait until the handle arrives at the target value before generating a new random value
        yield return new WaitUntil(() => !isMovingT);
        // Start the coroutine again to generate a new random value and move the handle towards it
        StartCoroutine(MoveTop());
    }
    IEnumerator MoveMiddle()
    {
        float targetValue = Random.Range(0, 10);// Generate a random value between minRange and maxRange
        isMovingM = true;// Move the handle towards the target value
        while (M_S.value != targetValue)
        {
            M_S.value = Mathf.MoveTowards(M_S.value, targetValue, MSpeed * Time.deltaTime);
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
        float targetValue = Random.Range(0, 10);// Generate a random value between minRange and maxRange
        isMovingB = true;// Move the handle towards the target value
        while (B_S.value != targetValue)
        {
            B_S.value = Mathf.MoveTowards(B_S.value, targetValue, BSpeed * Time.deltaTime);
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
            if (current == Top && pt.rect.Overlaps(t.rect)) //We have to check the certain angle range of values, collisions will mess UI up, and sorry its the simplest way for now :(
            {
                anim.SetTrigger("pulse");
                list[iterator].gameObject.SetActive(false);
                unlockSound.Play();
                iterator++;
                Debug.Log("layer1");
            } else if (current == Mid && pm.rect.Overlaps(m.rect)) //We have to check the certain angle range of values, collisions will mess UI up, and sorry its the simplest way for now :(
            {
                anim.SetTrigger("pulse");
                list[iterator].gameObject.SetActive(false);
                unlockSound.Play(); 
                iterator++;
                Debug.Log("layer2");
            }
            else if (current == Bottom && pb.rect.Overlaps(b.rect)) //We have to check the certain angle range of values, collisions will mess UI up, and sorry its the simplest way for now :(
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

    bool IsOverlapping(RectTransform rectTransform1, RectTransform rectTransform2)
    {
        Debug.Log("doing it");
        // Get the corners of the first rect transform
        Vector3[] corners1 = new Vector3[4];
        rectTransform1.GetWorldCorners(corners1);

        // Get the corners of the second rect transform
        Vector3[] corners2 = new Vector3[4];
        rectTransform2.GetWorldCorners(corners2);

        // Check if the rectangles overlap
        return (corners1[0].x < corners2[3].x && corners1[3].x > corners2[0].x && corners1[0].y < corners2[3].y && corners1[3].y > corners2[0].y);
    }
}

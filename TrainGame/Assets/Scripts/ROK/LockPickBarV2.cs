using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LockPickBarV2 : MonoBehaviour
{
    public int LayerNum;
    [SerializeField] private GameObject LayerPrefab;
    [SerializeField] private GameObject LayerHolder;
    [SerializeField, ReadOnly, BoxGroup("Assignment")] private List<GameObject> GeneratedLayer = new List<GameObject>(); 
    [SerializeField, BoxGroup("Assignment")] private List<Color> BlockColor = new List<Color>();
    private List<GameObject> MovingBlock = new List<GameObject>();  //the handle
    private List<GameObject> Detector = new List<GameObject>();  //the handle
    private List<GameObject> Lock = new List<GameObject>();  //the handle
    [SerializeField] private Sprite unlockImg;
    
    public bool Complete;
    //[SerializeField] List<GameObject> list;
    public int iterator;
    [ReadOnly]public int current;
    [SerializeField] AudioSource unlockSound;
    Animator anim;
//The RectTransform
    private List<RectTransform> MovingBlock_rect = new List<RectTransform>();   
    private List<RectTransform> Detector_rect = new List<RectTransform>();

    List<float> MovingBlock_pos = new List<float>(); //not using
    List<float> MovingBlock_width = new List<float>(); //not using
    List<bool> MovingBlock_IsMoving = new List<bool>();

    private Vector2 touchMargin;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        for(int i = 0; i < LayerNum; i++){ //initatiate it
            GameObject l = Instantiate(LayerPrefab, LayerHolder.transform, false) as GameObject;
            l.transform.parent = LayerHolder.transform;
            GeneratedLayer.Add(l);
            //get random width 
            MovingBlock_IsMoving.Add(false);
        }
        for(int i = 0; i < GeneratedLayer.Count; i++){ //assignments
            GameObject BG = GeneratedLayer[i].transform.GetChild(0).gameObject;
            MovingBlock.Add(BG.transform.GetChild(0).gameObject);
            Detector.Add(GeneratedLayer[i].transform.GetChild(1).gameObject);
            Lock.Add(GeneratedLayer[i].transform.GetChild(2).gameObject);
        }
        for(int i = 0; i < MovingBlock.Count; i++){
            MovingBlock[i].GetComponent<Image>().color = BlockColor[i];
            MovingBlock_rect.Add(MovingBlock[i].GetComponent<RectTransform>());
            Detector_rect.Add(Detector[i].GetComponent<RectTransform>());
        }
//-------- Set Size
        iterator = 0;
        for(int i = 0; i < MovingBlock_rect.Count; i++){
            if(i>=0 || i<2){
              MovingBlock_rect[i].sizeDelta = new Vector2(SetRandomBarWidth(30, 200), MovingBlock_rect[i].sizeDelta.y);  
            }
            
            MovingBlock_rect[i].anchoredPosition = new Vector2(Random.Range(-151, 151), MovingBlock_rect[i].anchoredPosition.y);
        }

        // t.sizeDelta = new Vector2(SetRandomBarWidth(150, 200), t.sizeDelta.y);
        // m.sizeDelta = new Vector2(SetRandomBarWidth(100, 150), m.sizeDelta.y);
        // b.sizeDelta = new Vector2(SetRandomBarWidth(30, 120), b.sizeDelta.y);

        // t.anchoredPosition = new Vector2(Random.Range(-151, 151), t.anchoredPosition.y);
        // m.anchoredPosition = new Vector2(Random.Range(-151, 151), m.anchoredPosition.y);
        // b.anchoredPosition = new Vector2(Random.Range(-151, 151), b.anchoredPosition.y);
        for(int i = 0; i < GeneratedLayer.Count; i++){
           StartCoroutine(MoveBar(MovingBlock_rect[i], MovingBlock_IsMoving[i], SetRandomSpeed(150,300))); //later put this in for loop 
        }
        
    }

    void Update()
    {
        current = iterator;
        for(int i = 0; i<GeneratedLayer.Count;i++){
            CheckLockBar(i,MovingBlock_rect[i], Detector_rect[i]);
        }
        
    }

    IEnumerator MoveBar(RectTransform MB_rect, bool isMovingT, float _speed)  //-151, 151
    {
        float targetValue = Random.Range(-151, 151);// Generate a random value between minRange and maxRange
        Vector2 targetPosition = new Vector2(targetValue, MB_rect.anchoredPosition.y);
        isMovingT = true;// Move the handle towards the target value
        while (MB_rect.anchoredPosition.x != targetValue)
        {
            //t.anchoredPosition.x = Mathf.MoveTowards(t.anchoredPosition.x, targetValue, TSpeed * Time.deltaTime);
            MB_rect.anchoredPosition = Vector2.MoveTowards(MB_rect.anchoredPosition, targetPosition, _speed * Time.deltaTime);
            yield return null;
        }
        Debug.Log("done");
        isMovingT = false;
        // Wait until the handle arrives at the target value before generating a new random value
        yield return new WaitUntil(() => !isMovingT);
        // Start the coroutine again to generate a new random value and move the handle towards it
        StartCoroutine(MoveBar(MB_rect, isMovingT, _speed)); //make it continue repeat
    }
    void CheckLockBar(int _current, RectTransform MovingBlockR, RectTransform DetectorR){
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (current == _current && CheckOverlap(MovingBlockR, DetectorR)) //We have to check the certain angle range of values, collisions will mess UI up, and sorry its the simplest way for now :(
                {
                    //anim.SetTrigger("pulse");
                    //StartCoroutine(CloseLock(2f, Lock[_current]));
                    Lock[_current].GetComponent<Image>().sprite = unlockImg;
                    GeneratedLayer[_current].GetComponent<Animator>().SetTrigger("Off");
                    MovingBlock[_current].SetActive(false);
                    unlockSound.Play();
                    iterator++;
                    Debug.Log("layer1");
                    if(current==GeneratedLayer.Count-1){Complete = true;};
                } else{
                    GeneratedLayer[_current].GetComponent<Animator>().SetTrigger("Click");
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
    float SetRandomSpeed(int min, int max){
        float ranSpeed = Random.Range(min,max);
        return ranSpeed;
    }
    bool CheckOverlap(RectTransform MUI, RectTransform SUI){
        Rect staticRect = new Rect(SUI.GetComponent<RectTransform>().anchoredPosition - SUI.GetComponent<RectTransform>().rect.size / 2, SUI.GetComponent<RectTransform>().rect.size);
        Rect movingRect = new Rect(MUI.anchoredPosition - MUI.rect.size / 2,MUI.rect.size);

        touchMargin.x = (MUI.rect.width + SUI.GetComponent<RectTransform>().rect.width) / 2;
        touchMargin.y = (MUI.rect.height + SUI.GetComponent<RectTransform>().rect.height) / 2;
        // Check if the two UI elements are touching/overlapping
        return staticRect.Overlaps(movingRect) && 
            Mathf.Abs(MUI.anchoredPosition.x - SUI.GetComponent<RectTransform>().anchoredPosition.x) < touchMargin.x &&
            Mathf.Abs(MUI.anchoredPosition.y - SUI.GetComponent<RectTransform>().anchoredPosition.y) < touchMargin.y;
    }
    // IEnumerator CloseLock(float sec, GameObject _lock){
    //     yield return new WaitForSeconds(sec);
    //     _lock.SetActive(false);
    //     //particles
    // }
}


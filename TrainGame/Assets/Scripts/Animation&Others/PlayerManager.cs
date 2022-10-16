using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script handle wtever related to Player that is not related to topdown engine
public class PlayerManager : MonoBehaviour
{
    [Header("Assignment")]
    [SerializeField] private GameObject FrontMC;
    [SerializeField] private GameObject BackMC;
    [SerializeField] private GameObject dustPrefab; //the particle system: prefab
    [SerializeField] private GameObject emitPt; //the particle system: prefab
    public List<GameObject> dust = new List<GameObject>();
    //public Character CharacterSc;

    float x;
    float y;
    float z;
    private float oldPositionX = 0.0f;
    private float oldPositionZ = 0.0f;
    void Start()
    {
        //CharacterSc = this.GameObject.GetComponent<Character>();
        if(FrontMC!=null){
            FrontMC.SetActive(true);
            BackMC.SetActive(false);
        }
    }
    void Update()
    {
        if (transform.position.z < oldPositionZ && FrontMC!=null) //Change player gameObject
        {
            FrontMC.SetActive(true);
            BackMC.SetActive(false);
            DustEmit();
            DustLayerSort(-1);
            //this.GetComponent<Character>().CharacterAnimator = FrontMC.GetComponent<Animator>();
        }else if (transform.position.z > oldPositionZ && FrontMC!=null)
        {
            FrontMC.SetActive(false);
            BackMC.SetActive(true);
            DustEmit();
            DustLayerSort(1);
            //CharacterSc.CharacterAnimator = BackMC.GetComponent<Animator>();
        }
        if (transform.position.x > oldPositionX) //Rotate the anim object instead of main
        {
            DustLayerSort(-1);
            DustEmit();
        }else if (transform.position.x < oldPositionX)
        {
            DustLayerSort(-1);
            DustEmit();
        }
        oldPositionX = transform.position.x;
        oldPositionZ = transform.position.z;
    }
    public void DustEmit(){
        if(Time.frameCount%10 == 0 && emitPt!=null){
            GameObject d = Instantiate(dustPrefab, emitPt.transform.position, Quaternion.identity) as GameObject;
            dust.Add(d);
            Invoke("DestroyDust", 1f);
        }
    }
    private void DestroyDust(){
        Destroy(dust[0]);
        dust.RemoveAt(0);
    }
    private void DustLayerSort(int order){
        for(int i = 0; i < dust.Count; i++){
                dust[i].GetComponent<ParticleSystemRenderer>().sortingOrder = order;
        }
    }

}

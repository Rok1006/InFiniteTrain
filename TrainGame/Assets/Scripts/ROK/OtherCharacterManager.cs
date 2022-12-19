using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//This script handle the depth detect, effect emit of other character(excluding players)
public class OtherCharacterManager : MonoBehaviour
{
    public enum CharacterType { NONE, NPC, ENEMY, DRONES};
    public CharacterType currentCharacterType = CharacterType.NONE;

    [Header("Assignment")]
    [SerializeField] private GameObject dustPrefab; //the particle system: prefab
    [SerializeField] private GameObject emitPt; //the particle system: prefab
    [SerializeField] private GameObject depthDetect;
    [HideInInspector]public List<GameObject> dust = new List<GameObject>();

    public float rayLength = 30f;

    void Start()
    {
        
    }

    void Update()
    {
        // DustEmit();   do this when move
        // DustLayerSort(-1);
    }
    public void DustEmit(){
        if(Time.frameCount%10 == 0 && emitPt!=null){
            GameObject d = Instantiate(dustPrefab, emitPt.transform.position, Quaternion.identity) as GameObject;
            d.transform.parent = emitPt.transform;
            dust.Add(d);
            Invoke("DestroyDust", .9f);
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
    //DepthDetect
}

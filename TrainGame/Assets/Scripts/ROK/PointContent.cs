using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
// [CreateAssetMenu(fileName = "NewPointData", menuName = "ScriptableObject/PointData")]
public class PointContent : MonoBehaviour
{
    [SerializeField, BoxGroup("PointInfo")]private int numberOfArea;
    [SerializeField, BoxGroup("PointInfo")]private List<GameObject> ResourcesBoxPoint = new List<GameObject>();
    public List<boundaryAreaClass> BoundaryArea = new List<boundaryAreaClass>();

    [SerializeField, BoxGroup("Resources")]private GameObject[] TrapTileType;
    [SerializeField, BoxGroup("Resources")]private GameObject[] GrassType; //grass prefab for generating grass or interactable environemnt
    [SerializeField, BoxGroup("Resources")]private GameObject[] ResourceBoxType;

    [SerializeField, BoxGroup("Setting")]private int GrassAmt;

    float minX,maxX,minZ,maxZ = 0;

    private void Start() {
        minX = Mathf.Infinity;
        maxX = -Mathf.Infinity;
        minZ = Mathf.Infinity;
        maxZ = -Mathf.Infinity;

        // for(int x = 0; x<BoundaryArea.Count;x++){ //gp in order look into the bigger list 
        //     for(int y = 0; y<BoundaryArea[x].Count;y++){  //how to check through a classs
        //         FindMaxnMin(BoundaryArea[x][y]);
        //     }
        // }
        
    }

    void FindMaxnMin(List<GameObject> innerList){
        foreach (GameObject point in innerList)
        {
            // if (point.x < minX)
            // {
            //     minX = point.x;
            // }

            // if (point.x > maxX)
            // {
            //     maxX = point.x;
            // }

            // if (point.z < minZ)
            // {
            //     minZ = point.z;
            // }

            // if (point.z > maxZ)
            // {
            //     maxZ = point.z;
            // }
        }
    }

}

[System.Serializable]
    public class boundaryAreaClass
    {
        public List<GameObject> BoundaryPt;
    }
    

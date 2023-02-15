using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
// [CreateAssetMenu(fileName = "NewPointData", menuName = "ScriptableObject/PointData")]
//To do: 
//detect if spawned item are overlapping with eachother by checking their radius?
//spawning enviromental object
//** Shd deal with deapth issue of spawned object: resource box, plant
public class PointContent : MonoBehaviour
{
    [SerializeField, BoxGroup("PointInfo")]private int numberOfArea;
    [SerializeField, BoxGroup("PointInfo")]private List<GameObject> ResourcesBoxPoint = new List<GameObject>();
    [SerializeField, BoxGroup("PointInfo")] public List<boundaryAreaClass> BoundaryArea = new List<boundaryAreaClass>();
    [SerializeField, BoxGroup("PointInfo")] private List<Vector3> RandomPoint = new List<Vector3>();
    Vector3 previousPt;
    [SerializeField, BoxGroup("Resources")]private GameObject[] TrapTileType;
    [SerializeField, BoxGroup("Resources")]private GameObject[] ResourceBoxType;

    [SerializeField, BoxGroup("GrassSetting")]private int GrassAmt;
    [SerializeField, BoxGroup("GrassSetting")]private GameObject[] GrassType; //grass prefab for generating grass or interactable environemnt

    public float minX,maxX,minZ,maxZ = 0;

    private void Start() {
        minX = Mathf.Infinity;
        maxX = -Mathf.Infinity;
        minZ = Mathf.Infinity;
        maxZ = -Mathf.Infinity;

        foreach (boundaryAreaClass BA in BoundaryArea) //look through each class list
        {
            //Debug.Log(myClass.name + "'s list:");
            for (int i = 0; i < BA.BoundaryPt.Count; i++)
            {
                Debug.Log(BA.BoundaryPt[i]);
                FindMaxnMin(BA.BoundaryPt[i]);
            }
            for (int i = 0; i < GrassAmt; i++){ //needa make sure that the generated pt is not the same
                Vector3 currentPt = GetRandomPt(BA.BoundaryPt);
                //GetRandomPt(BA.BoundaryPt);
                if(currentPt==previousPt){
                    currentPt = GetRandomPt(BA.BoundaryPt);
                }else{
                    RandomPoint.Add(currentPt);
                }
                GameObject g = Instantiate (GrassType[Random.Range(0, GrassType.Length)], currentPt, Quaternion.identity);
                g.transform.localScale = new Vector3(7f,7f,7f); //currently hard code if have more types gotta figure out a way
                g.transform.rotation = Quaternion.Euler(100f, 0f, 180f);
            }
        }
    }

    void FindMaxnMin(GameObject pt){
            Vector3 point = pt.transform.position;
            if (point.x < minX)
            {
                minX = point.x;
            }
            if (point.x > maxX)
            {
                maxX = point.x;
            }
            if (point.z < minZ)
            {
                minZ = point.z;
            }
            if (point.z > maxZ)
            {
                maxZ = point.z;
            }
    }
    Vector3 GetRandomPt(List<GameObject> boundaryPoints ){
        Vector3 randomPoint;
        do{
            float randomX = Random.Range(minX, maxX);
            float randomZ = Random.Range(minZ, maxZ);
            randomPoint = new Vector3(randomX, 0f, randomZ);
        } while (!IsPointInsideBoundary(randomPoint, boundaryPoints));
        return randomPoint;
    }
    bool IsPointInsideBoundary(Vector3 point, List<GameObject> boundary){ // Define a function to check if a point is inside the boundary
        int count = 0;
        for (int i = 0; i < boundary.Count; i++)
        {
            Vector3 p1 = boundary[i].transform.position;
            Vector3 p2 = boundary[(i + 1) % boundary.Count].transform.position;
            if (IsPointOnLineSegment(point, p1, p2))
            {
                return true;
            }
            if (IsPointToLeftOfLine(point, p1, p2))
            {
                count++;
            }
        }
        return count % 2 == 1;
    }
    bool IsPointOnLineSegment(Vector3 point, Vector3 p1, Vector3 p2) // Define a function to check if a point is on a line segment
    {
        float d = Vector3.Distance(p1, p2);
        float d1 = Vector3.Distance(point, p1);
        float d2 = Vector3.Distance(point, p2);
        return Mathf.Approximately(d, d1 + d2);
    }
    bool IsPointToLeftOfLine(Vector3 point, Vector3 p1, Vector3 p2) // Define a function to check if a point is to the left of a line
    {
        return ((p2.x - p1.x) * (point.z - p1.z) - (p2.z - p1.z) * (point.x - p1.x)) > 0;
    }


}

[System.Serializable]
    public class boundaryAreaClass
    {
        public List<GameObject> BoundaryPt;
    }
    

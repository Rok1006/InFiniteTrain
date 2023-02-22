using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
// This script pull data frm pointData scObj and partly determine the content of whole pt., and store resources used in the pt
//To do: 
//detect if spawned item are overlapping with eachother by checking their radius?
//spawning enviromental object
//** Shd deal with deapth issue of spawned object: resource box, plant
//second land shd be spawned too, but first land shd always be the same
//For each pt it shd always end with land that have one entrance/exit
public class PointContent : MonoBehaviour
{
    [Space(10)][TextArea(3, 10)]public string Reminders;
    //1. The BoundaryArea List count shd equal to numberOfArea.
    [Tooltip("For Display Purpose")][SerializeField, BoxGroup("PointInfo")] private int numberOfArea;
    [SerializeField, BoxGroup("PointInfo")]private GameObject[] ActiveLand;
    [Tooltip("Assign all needed point data accord to numOfArea")][SerializeField, BoxGroup("PointInfo")]private PointData[] P_Data; 
    
    [SerializeField, BoxGroup("PointInfo")]private List<GameObject> ResourcesBoxPoint = new List<GameObject>();
    [Tooltip("Assign all corner pts frm heiarchy")][SerializeField, BoxGroup("PointInfo")] public List<boundaryAreaClass> BoundaryArea = new List<boundaryAreaClass>();
    [SerializeField, BoxGroup("PointInfo")] private List<Vector3> RandomPoint = new List<Vector3>();
    [SerializeField] private GameObject WalkingArea;
    Vector3 previousPt;
    List<GameObject> CreatedGrass = new List<GameObject>();  //list for created grass
    List<GameObject> CreatedStuff = new List<GameObject>();
    List<GameObject> DepthDetectStuff = new List<GameObject>();
    List<GameObject> CurrentList = new List<GameObject>();
    [SerializeField] private List<bool> IsPointFull = new List<bool>();

    [SerializeField, BoxGroup("Resources")]private GameObject[] TrapTileType;
    [SerializeField, BoxGroup("Resources")]private GameObject[] ResourceBoxType;
    [SerializeField, BoxGroup("Resources")]private GameObject[] GrassType; //grass prefab for generating grass or interactable environemnt
    [SerializeField, BoxGroup("Resources")]private GameObject[] PondType;
    //[SerializeField, BoxGroup("GrassSetting")]private int GrassAmt;
    [ReadOnly]public float minX,maxX,minZ,maxZ = 0;
    bool canCheckOverlap = false;
    int depth = 0;
    int count = 0;//num of data
    private void Awake() {
        for(int a = 0; a<ActiveLand.Length; a++){
            ActiveLand[a].SetActive(true);
        }
    }
    private void Start() {
        minX = Mathf.Infinity;
        maxX = -Mathf.Infinity;
        minZ = Mathf.Infinity;
        maxZ = -Mathf.Infinity;
        
        GenerateContent();
        if(WalkingArea!=null){
            CreatedStuff.Add(WalkingArea);
            CreatedGrass.Add(WalkingArea);  
        }
        
    }
    private void Update() {
        if(canCheckOverlap){
            // foreach (boundaryAreaClass BA in BoundaryArea) //look through each class list
            // { 
                // int iteration = 0;
                // if(iteration==count){
                CheckIfOverlap(CreatedStuff, CurrentList); //Fixed
                CheckIfOverlap(CreatedGrass, CurrentList);
            //}
            canCheckOverlap = false;
        };
        
    }
    void GenerateContent(){ //Main Body
    // int count = 0;
        foreach (boundaryAreaClass BA in BoundaryArea) //look through each class list
        { //Debug.Log(myClass.name + "'s list:");
            Debug.Log("checked");
            for (int i = 0; i < BA.BoundaryPt.Count; i++) //Assign Min Max
            {
                Debug.Log(BA.BoundaryPt[i]);
                FindMaxnMin(BA.BoundaryPt[i]);
            }
//Enviroment Related-------------------------
            CurrentList = BA.BoundaryPt;
            SpawnGrass(count, BA.BoundaryPt);
            SpawnTraps(count, BA.BoundaryPt);
            if(P_Data[count].haveResources){
                for(int a = 0; a<ResourcesBoxPoint.Count; a++){
                    IsPointFull.Add(false);
                }
                SpawnResouceBox(count);
            };
            if(P_Data[count].havePond){SpawnPond(BA.BoundaryPt);};
            count+=1; //Change Data files, make sure there is correct num of data
        }
    }
    void AssignDepthLayer(GameObject j){ //put this in a for loop check the spawned obj with the created list evertime if the Z is more front assign a higher number
        //j is the newly instanciated obj
        //j.GetComponent<SpriteRenderer>().sortingOrder = 0;
        for(int i = 0; i<DepthDetectStuff.Count; i++){
            if(j.transform.position.z > DepthDetectStuff[i].transform.position.z){ //is at the back
                j.GetComponent<SpawnedStuff>().order = DepthDetectStuff[i].GetComponent<SpawnedStuff>().order-1;
            }else if(j.transform.position.z < DepthDetectStuff[i].transform.position.z){ //is in front
                j.GetComponent<SpawnedStuff>().order = DepthDetectStuff[i].GetComponent<SpawnedStuff>().order+1;
            }
        }
    }
    void CheckIfOverlap(List<GameObject> ListChecking, List<GameObject> BA){ //For traps
        int overLapCount = 0;
        for (int i = 0; i < ListChecking.Count - 1; i++)
        {
            for (int j = i + 1; j < ListChecking.Count; j++)
            {
                Collider collider1 = ListChecking[i].GetComponent<Collider>();
                Collider collider2 = ListChecking[j].GetComponent<Collider>();

                if (collider1 != null && collider2 != null && collider1.bounds.Intersects(collider2.bounds))
                {
                    overLapCount+=1;
                    
                    Vector3 newPt = GetRandomPt(BA);  //this is get all the other land pt too, get specific land
                    Debug.Log(ListChecking[i].gameObject.name);
                    if(WalkingArea!=null){
                        ListChecking[i].SetActive(false); ///currently only disable them but need to think abt how to generate new ones at the missing point
                    }else{
                        ListChecking[i].transform.position = newPt; //find a way to redo
                    }
                }
            }
        }
        //return false;   
    }
    void SpawnGrass(int count, List<GameObject> BA){ //count is the spawned land, each data count is one land, each pt can have multiple land
        RandomPoint.TrimExcess(); //Reset list
        RandomPoint.Clear(); //Reset list
        for (int i = 0; i < P_Data[count].GrassAmt; i++){ //needa make sure that the generated pt is not the same
                Vector3 currentPt = GetRandomPt(BA);
                if(currentPt==previousPt){
                    currentPt = GetRandomPt(BA);
                }else{
                    RandomPoint.Add(currentPt);
                }
                GameObject g = Instantiate (GrassType[Random.Range(0, GrassType.Length)], currentPt, Quaternion.identity);
                var sc = g.GetComponent<SpawnedStuff>();
                //g.transform.localScale = new Vector3(7f,7f,7f); //currently hard code if have more types gotta figure out a way
                //g.transform.rotation = g.GetComponent<SpawnedStuff>().rotation;
                
                CreatedGrass.Add(g);
                if(i == P_Data[count].GrassAmt-1){ //wait until the last check
                    canCheckOverlap = true;
                }
                DepthDetectStuff.Add(g); //add it to assign depth
                AssignDepthLayer(g);
        }
    }//saveable
    void SpawnTraps(int count, List<GameObject> BA){ //count is the spawned land, each data count is one land, each pt can have multiple land
        RandomPoint.TrimExcess(); //Reset list //to get new random pts
        RandomPoint.Clear(); //Reset list
        CreatedStuff.TrimExcess(); //Reset list //to get new random pts
        CreatedStuff.Clear(); //Reset list
        for (int i = 0; i < P_Data[count].TrapAmt; i++){ //needa make sure that the generated pt is not the same
                Vector3 currentPt = GetRandomPt(BA);
                previousPt = currentPt;
                RandomPoint.Add(currentPt);
                GameObject t = Instantiate (TrapTileType[Random.Range(0, TrapTileType.Length)], currentPt, Quaternion.identity);
                t.transform.rotation = Quaternion.Euler(90f, 0f, 0f); //only certain type is like that
                CreatedStuff.Add(t);
                if(i == P_Data[count].TrapAmt-1){ //wait until the last check
                    canCheckOverlap = true;
                }
        }
        //CheckIfOverlap();
    }//saveable
    void SpawnPond(List<GameObject> BA){
        Vector3 currentPt = GetRandomPt(BA);
        GameObject t = Instantiate (PondType[Random.Range(0, PondType.Length)], currentPt, Quaternion.identity);
        int ran = Random.Range(2,5);
        t.transform.localScale = new Vector3(ran,ran,ran);
        CreatedStuff.Add(t);
        canCheckOverlap = true;
    }//saveable
    void SpawnResouceBox(int count){ //cannot spawn on the same pt
        Vector3 getAPt;
        int index;
        for(int x = 0; x < P_Data[count].resourceBoxNum; x++){
            for(int y = 0; y < IsPointFull.Count; y++){
                index = Random.Range(0,ResourcesBoxPoint.Count);
                if(IsPointFull[index]==false){
                    getAPt = ResourcesBoxPoint[index].transform.position;
                    GameObject r = Instantiate (ResourceBoxType[Random.Range(0, ResourceBoxType.Length)], getAPt, Quaternion.identity);
                    IsPointFull[index]=true;
                    break;
                }
            }
        }
    }//saveable
    // bool CheckIfSameLocation(Vector3 point, List<GameObject> ResourcePt){
    //     int num = 0;
    //     bool isConflict = false;
    //     foreach(GameObject r in ResourcePt){
    //         if(point == r.transform.position){ //this is not right cus it will always be true everyone of them
    //             Debug.Log("overlapped");
    //             isConflict = true;
    //             //return false; //found overlap
    //         }
    //         // else{
    //         //     num++;
    //         // }
    //     }
    //     return isConflict;
    //     //return num % 2 == 1; //after checked all and found no overlap
    // }
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
        int num = 0;
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
                //Debug.Log("jsehfjskdh");
                num++;
            }
        }
        return num % 2 == 1;
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
    
/* Current Issue:
- SomeTime Grass out of BoundaryPt, why?
*/
// for (int i = 0; i < P_Data[count].GrassAmt; i++){ //needa make sure that the generated pt is not the same
            //     Vector3 currentPt = GetRandomPt(BA.BoundaryPt);
            //     if(currentPt==previousPt){
            //         currentPt = GetRandomPt(BA.BoundaryPt);
            //     }else{
            //         RandomPoint.Add(currentPt);
            //     }
            //     GameObject g = Instantiate (GrassType[Random.Range(0, GrassType.Length)], currentPt, Quaternion.identity);
            //     g.transform.localScale = new Vector3(7f,7f,7f); //currently hard code if have more types gotta figure out a way
            //     g.transform.rotation = Quaternion.Euler(70f, 0f, 180f);
            //     CreatedStuff.Add(g);
            // }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MoreMountains.TopDownEngine;
using NaughtyAttributes;

/* this class is aiming to control player UI*/
public class UIControl : MonoBehaviour
{
    [SerializeField, BoxGroup("World Map")] private GameObject worldMap;
    [SerializeField, BoxGroup("Train Map")] private GameObject trainMap, trainIconPanel, playerIcon;
    [SerializeField, BoxGroup("Train Map"), InfoBox("order from far right to far left", EInfoBoxType.Normal)] private List<TrainMapTrainIcon> trainMapTrainIcons;

    //managers references
    private RoomManager roomManager;

    void Start() {
        roomManager = FindObjectOfType<RoomManager>();
        TrainMapSetUp();
    }

    private void TrainMapSetUp() {
        for (int i = 0; i < roomManager.CarList.Count; i++) {
            if (trainMapTrainIcons.Count > i)
                roomManager.CarList[i].TrainMapIcon = trainMapTrainIcons[i];
            else
                Debug.Log("there's more car (" + roomManager.CarList.Count + ") than train map train icons (" + trainMapTrainIcons.Count +") ");
        }
    }

    /*  inside train map
        move player icon to corresponding car's train map train icon*/
    public void MoveTrainMapPlayerIcon(Car car) {
        playerIcon.transform.SetParent(car.TrainMapIcon.Center.transform);
        playerIcon.transform.localPosition = Vector3.zero;
    }
}

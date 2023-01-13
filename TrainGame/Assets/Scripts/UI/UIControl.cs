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
        trainMapSetUp();
    }

    private void trainMapSetUp() {
        for (int i = 0; i < roomManager.CarList.Count; i++) {
            roomManager.CarList[i].TrainMapIcon = trainMapTrainIcons[i];
        }
    }
}

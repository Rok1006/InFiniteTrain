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
    [SerializeField, BoxGroup("Train Map")] private GameObject trainIconPanel, playerIcon;
}

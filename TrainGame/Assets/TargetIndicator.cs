using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetIndicator : MonoBehaviour
{
    public Transform target;
    public RectTransform uiObject;
    public Vector2 padding;
    public GameObject player;
    private Camera mainCamera;
    private RectTransform canvasRect;
    private Vector2 screenBounds;
    private float width;
    private float height;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        canvasRect = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
        screenBounds = new Vector2(Screen.width, Screen.height);
        width = canvasRect.sizeDelta.x;
        height = canvasRect.sizeDelta.y;
        Debug.Log(width + " " + height);
        uiObject.anchoredPosition = mainCamera.WorldToScreenPoint(player.transform.position);
    }

    void Update()
    {
        if(Tutorial.instance.arrow == null)
        {
            Tutorial.instance.arrow = this;
        }
        var playertransform =  mainCamera.WorldToViewportPoint(player.transform.position);
        var CanvasPosition = new Vector3(playertransform.x * width/2, playertransform.y * height/2, 0);
        Vector2 viewportPos = Camera.main.WorldToViewportPoint(player.transform.position);
        Vector2 pixelPos = new Vector2(viewportPos.x * width, viewportPos.y * height);

        // Map the pixel position to the canvas coordinates
        float canvasX = (pixelPos.x / width) * width - (width * 0.5f);
        float canvasY = (pixelPos.y / height) * height - (height * 0.5f);

        // Set the player's position on the canvas
        uiObject.anchoredPosition = new Vector2(canvasX, canvasY);
        
        if(target != null)
        {
            var targetTransform = mainCamera.WorldToViewportPoint(target.transform.position);
            Vector2 targetCoord = new Vector2(targetTransform.x * width, targetTransform.y * height);
            float targetX = (targetCoord.x / width) * width - (width * 0.5f);
            float targetY = (targetCoord.y / height) * height - (height * 0.5f);

            var final = new Vector2(targetX, targetY);

            var dir = (final - uiObject.anchoredPosition).normalized;

            float rot_z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rot_z - 90);
            //uiObject.rotation = Quaternion.Euler(0, 0, GetAngleFromVector(dir));
        }
        /*
        Vector3 targetScreenPosition = mainCamera.WorldToScreenPoint(target.position);
        Debug.Log(targetScreenPosition);
        Vector2 uiScreenPosition = new Vector2(targetScreenPosition.x, targetScreenPosition.y);

        if (targetScreenPosition.z < 0) // target is behind the camera
        {
            Vector2 targetViewportPosition = mainCamera.WorldToViewportPoint(target.position);
            uiScreenPosition = new Vector2(
                (targetViewportPosition.x - 0.5f) * canvasRect.sizeDelta.x - padding.x,
                (targetViewportPosition.y - 0.5f) * canvasRect.sizeDelta.y - padding.y
            );
            uiScreenPosition = new Vector2(
                Mathf.Clamp(uiScreenPosition.x, -screenBounds.x / 2f, screenBounds.x / 2f),
                Mathf.Clamp(uiScreenPosition.y, -screenBounds.y / 2f, screenBounds.y / 2f)
            );
        }

        bool targetInCameraView = targetScreenPosition.z >= 0 && targetScreenPosition.x >= 0 && targetScreenPosition.x <= Screen.width && targetScreenPosition.y >= 0 && targetScreenPosition.y <= Screen.height;
        uiObject.gameObject.SetActive(!targetInCameraView);
        if (!targetInCameraView)
        {
            uiScreenPosition = new Vector2(
               Mathf.Clamp(uiScreenPosition.x, -screenBounds.x / 2f + padding.x, screenBounds.x / 2f - padding.x),
               Mathf.Clamp(uiScreenPosition.y, -screenBounds.y / 2f + padding.y, screenBounds.y / 2f - padding.y)
           );
            uiObject.anchoredPosition = uiScreenPosition;
            uiObject.rotation = Quaternion.Euler(0, 0, GetAngleFromVector(uiScreenPosition - new Vector2(Screen.width / 2f, Screen.height / 2f)));

        }
        */
    }

    float GetAngleFromVector(Vector2 dir)
    {
        float angle = Vector3.SignedAngle(this.transform.up, dir, this.transform.forward);
       // float angle = Mathf.Acos((Vector2.Dot(this.transform.up, dir)) / (GetVector2Magnitude(this.transform.up) * GetVector2Magnitude(dir)));
        return angle;
    }
    public float GetVector2Magnitude(Vector2 vector)
    {
        return Mathf.Sqrt(vector.x * vector.x + vector.y * vector.y);
    }
}

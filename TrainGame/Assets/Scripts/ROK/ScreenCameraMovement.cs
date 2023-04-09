using UnityEngine;

public class ScreenCameraMovement : MonoBehaviour
{
    public float cameraSpeed = 0.1f;   // The speed of the camera movement
    public float maxCameraDistance = 10f;   // The maximum distance the camera can move

    private float screenWidth;   // The width of the screen
    private float screenHeight;  // The height of the screen

    public Vector3 minCameraPos;
    public Vector3 maxCameraPos;


    void Start()
    {
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    void Update()
    {
        // Get the mouse position in screen coordinates
        float mouseX = Input.mousePosition.x;
        float mouseY = Input.mousePosition.y;

        // Calculate the center of the screen
        float centerX = screenWidth / 2f;
        float centerY = screenHeight / 2f;

        // Calculate the distance between the mouse position and the center of the screen
        float distanceX = Mathf.Clamp((mouseX - centerX) / centerX, -1f, 1f);
        float distanceY = Mathf.Clamp((mouseY - centerY) / centerY, -1f, 1f);

        // Calculate the camera's new position
        float newPositionX = transform.localPosition.x - (distanceX * cameraSpeed * maxCameraDistance);
        float newPositionY = transform.localPosition.y + (distanceY * cameraSpeed * maxCameraDistance);

        // Apply the new camera position
        //transform.localPosition = new Vector3(newPositionX, newPositionY, transform.localPosition.z);

         transform.position = new Vector3(Mathf.Clamp(newPositionX, minCameraPos.x, maxCameraPos.x),
            Mathf.Clamp(newPositionY, minCameraPos.y, maxCameraPos.y),
            Mathf.Clamp(transform.localPosition.z, minCameraPos.z, maxCameraPos.z));

    }
}

using UnityEngine;

public class camController : MonoBehaviour
{
    public Transform target; // The object to orbit around
    private float distance; // Initial distance from the target
    public float minDistance = 200.0f; // Minimum zoom distance
    public float maxDistance = 500.0f; // Maximum zoom distance
    public float zoomSpeed = 20.0f; // Zoom speed
    public float orbitSpeedX = 300; // Horizontal orbit speed
    public float orbitSpeedY = 300; // Vertical orbit speed
    public float minYAngle = -80f; // Minimum vertical angle
    public float maxYAngle = 80f; // Maximum vertical angle

    private float currentX = 0.0f;
    private float currentY = 0.0f;

    void Update()
    {
        // Handle right mouse button for orbiting
        if (Input.GetMouseButton(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // Get mouse input for orbiting
            currentX += Input.GetAxis("Mouse X") * orbitSpeedX * Time.deltaTime;
            currentY -= Input.GetAxis("Mouse Y") * orbitSpeedY * Time.deltaTime;
            currentY = Mathf.Clamp(currentY, minYAngle, maxYAngle);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // Handle scroll wheel input for zooming
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        distance -= scroll * zoomSpeed;
        distance = Mathf.Clamp(distance, minDistance, maxDistance);
    }

    void LateUpdate()
    {
        if (target)
        {
            // Calculate new camera position and rotation
            Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
            Vector3 position = rotation * new Vector3(0, 0, -distance) + target.position;

            transform.position = position;
            transform.LookAt(target);
        }
    }
}
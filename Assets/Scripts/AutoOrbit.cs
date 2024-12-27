using UnityEngine;

public class AutoOrbit : MonoBehaviour
{
    public Transform target; // The object to orbit around
    public float orbitSpeed = 50.0f; // Speed of the orbit
    public float orbitRadius = 5.0f; // Radius of the orbit
    public Vector3 orbitPlaneAxis = Vector3.up; // Orbit plane axis (allows arbitrary plane rotation)
    public float rotationSpeed = 20.0f; // Speed to rotate the orbit plane axis

    private float currentAngle = 0.0f;

    void Update()
    {
        if (target)
        {
            // Update the orbit angle
            currentAngle += orbitSpeed * Time.deltaTime;
            if (currentAngle >= 360.0f)
                currentAngle -= 360.0f;

            // Calculate the orbit position based on the plane's axis
            float radian = currentAngle * Mathf.Deg2Rad;
            Vector3 localPosition = new Vector3(Mathf.Cos(radian) * orbitRadius, 0, Mathf.Sin(radian) * orbitRadius);

            // Apply the arbitrary axis to the orbit plane
            Vector3 offset = Quaternion.FromToRotation(Vector3.up, orbitPlaneAxis) * localPosition;

            // Set the new position
            transform.position = target.position + offset;

            // Optionally face the target
            transform.LookAt(target);
        }
    }
}

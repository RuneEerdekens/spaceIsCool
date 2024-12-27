using UnityEditor.SearchService;
using UnityEngine;

public class EnemieBehaviour : MonoBehaviour
{
    public Transform target; // The planet or target the asteroid is being pulled towards
    public float gravityStrength = 10.0f; // Strength of the gravity pulling the asteroid
    public float initialSpeed = 5.0f; // Initial speed of the asteroid
    public float maxSpeed = 50.0f; // Maximum speed the asteroid can reach

    private Vector3 velocity;
    private Vector3 direction;

    void Start()
    {
        transform.LookAt(target);
        direction = (target.position - transform.position).normalized;
        velocity = direction * initialSpeed;
    }

    void Update()
    {
        if (target)
        {
            Vector3 directionToTarget = target.position - transform.position;

            directionToTarget.Normalize();

            Vector3 gravityForce = directionToTarget * gravityStrength / directionToTarget.sqrMagnitude;

            velocity += gravityForce * Time.deltaTime;

            velocity = Vector3.ClampMagnitude(velocity, maxSpeed);

            transform.position += velocity * Time.deltaTime;
        }
    }
}

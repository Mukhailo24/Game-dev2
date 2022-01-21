using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringAgent : MonoBehaviour
{
    [SerializeField]
    float friction;
    [SerializeField]
    public Vector3 velocity;
    [SerializeField]
    protected float maxVelocity;
    [SerializeField]
    float maxForce;
    [SerializeField]
    float axeleration = 3;
    [SerializeField]
    bool debugDraw = false;
    protected SteeringBehaviour currentBehavior;


    void Start()
    {
        currentBehavior = GetComponent<SteeringBehaviour>();
    }

    protected virtual void Update()
    {
        if (debugDraw) DebugDraw();

        ApplyFriction();

        Vector3 desiredVelocity = currentBehavior.CalculateDesiredVelocity();
        desiredVelocity.Scale(new Vector3(1f, 1f, 0f));
        desiredVelocity = desiredVelocity.normalized * maxVelocity;
        Vector3 steeringForce = Vector3.ClampMagnitude(desiredVelocity.normalized * maxVelocity - velocity, maxForce);

        ApplySteer(steeringForce);

        ChangeBehaviour();
    }

    void ApplySteer(Vector3 steeringForce)
    {
        velocity += steeringForce * axeleration * Time.deltaTime;
        velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
        if (!(velocity.magnitude < float.Epsilon))
        {
            transform.position += velocity * Time.deltaTime;
        }
    }

    protected virtual void ChangeBehaviour()
    {

    }

    void ApplyFriction()
    {
        velocity -= velocity.normalized * friction * Time.deltaTime;
    }

    void DebugDraw()
    {
        Debug.DrawLine(transform.position, transform.position + velocity, Color.green);
    }

    public virtual void Die() {
        Destroy(this.currentBehavior);
        Destroy(gameObject);
    }
}

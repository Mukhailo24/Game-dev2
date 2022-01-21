using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviour : MonoBehaviour
{
    protected Bounds gameArea;
    public float maxDirToWall = 1;

    void Start() {
        gameArea = FindObjectOfType<World>().gameArea;
    }


    public virtual Vector3 CalculateDesiredVelocity() {
        return Vector3.zero;
    }

    public Vector3 AvoidWalls() {
        gameArea = FindObjectOfType<World>().gameArea;
        Vector3 desiredVelocity = -(transform.position - gameArea.ClosestPoint(transform.position)).normalized;
        desiredVelocity.Scale(new Vector3(1f, 1f, 0f));
        return desiredVelocity;
    }
}

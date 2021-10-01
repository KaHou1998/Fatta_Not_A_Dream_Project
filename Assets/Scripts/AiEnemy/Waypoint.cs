using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public float range;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public Vector3 GenerateRandomPoint()
    {
        Vector3 randomPoint = this.transform.position + Random.insideUnitSphere * range;
        return randomPoint;
    }
}



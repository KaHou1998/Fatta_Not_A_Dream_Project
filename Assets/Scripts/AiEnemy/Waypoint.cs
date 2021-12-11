using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public float range;
    public float agentRange;
    private Vector3 prevStopVec;
    
    public void Awake()
    {
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public Vector3 GenerateRandomPoint(float directionX, float directionZ)
    {
        //Vector3 randomPoint;
        //randomPoint = this.transform.position + Random.insideUnitSphere * range;
        Vector3 randomPoint;
        randomPoint = new Vector3(Random.Range(this.transform.position.x, this.transform.position.x + (range * directionX / 2)),
            this.transform.position.y, Random.Range(this.transform.position.z, this.transform.position.z + (range * directionZ/ 2)));
        Debug.Log("Random Point: " + randomPoint);

        return randomPoint;
    }

    public Vector3 getPrevVec()
    {
        return prevStopVec;
    }
}



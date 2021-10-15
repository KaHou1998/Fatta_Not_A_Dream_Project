using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    public struct PatrolProperty
    {
        public Vector3 patrolVector;
        public bool available;
    }

    public float range;
    public float agentRange;
    private Vector3 prevStopVec;

    public PatrolProperty[] patrolProperty = new PatrolProperty[4];
    

    public void Awake()
    {
        InitPatrolPoint();
    }

    void InitPatrolPoint()
    {
        patrolProperty[0].patrolVector = new Vector3(Random.Range(this.transform.position.x - range, this.transform.position.x), this.transform.position.y, Random.Range(this.transform.position.z, this.transform.position.z + range));
        patrolProperty[1].patrolVector = new Vector3(Random.Range(this.transform.position.x - range, this.transform.position.x), this.transform.position.y, Random.Range(this.transform.position.z, this.transform.position.z - range));
        patrolProperty[2].patrolVector = new Vector3(Random.Range(this.transform.position.x + range, this.transform.position.x), this.transform.position.y, Random.Range(this.transform.position.z, this.transform.position.z + range));
        patrolProperty[3].patrolVector = new Vector3(Random.Range(this.transform.position.x + range, this.transform.position.x), this.transform.position.y, Random.Range(this.transform.position.z, this.transform.position.z - range));
        patrolProperty[0].available = true; 
        patrolProperty[1].available = true; 
        patrolProperty[2].available = true; 
        patrolProperty[3].available = true; 
       
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

    public Vector3 AvailablePoint()
    {
        for (int i = 0; i < patrolProperty.Length; i++)
        {
            if (patrolProperty[i].available)
            {
                patrolProperty[i].available = false;
                return patrolProperty[i].patrolVector;
            }
        }
        return Vector3.zero;
    }

    public void ExitVector(Vector3 prevVector)
    {
        for (int i = 0; i < patrolProperty.Length; i++)
        {
            if(patrolProperty[i].patrolVector == prevVector)
                patrolProperty[i].available = true;
        }

    }

    public void setPrevVec(Vector3 newVector)
    {
        prevStopVec = newVector;
    }

    public Vector3 getPrevVec()
    {
        return prevStopVec;
    }
}



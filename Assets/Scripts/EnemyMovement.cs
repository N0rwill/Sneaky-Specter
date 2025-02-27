using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 


public class EnemyMovement : MonoBehaviour 
{
    public NavMeshAgent agent;
    public float range; //radius of sphere

    public Transform centrePoint; //centre of the area the agent wants to move around in
    //instead of centrePoint you can set it as the transform of the agent if you don't care about a specific area
    public Transform player;
    public float fieldOfView = 120f;
    private bool playerInSight;


    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }


    void Update()
    {
        if (playerInSight)
        {
            chase();
        }
        else
        {
            RandomMove();
        }

    }
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }

    private void RandomMove()
    {
        if (agent.remainingDistance <= agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                agent.SetDestination(point);
            }
        }
    }

    private void chase()
    {
        agent.SetDestination(player.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.transform == player)
        {
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fieldOfView * 0.5f)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, direction.normalized, out hit, range))
                {
                    if (hit.transform == player)
                    {
                        playerInSight = true;
                        Debug.Log("Player in sight");
                    }
                }
            }
        
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            playerInSight = false;
        }
    }


}
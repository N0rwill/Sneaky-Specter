using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = System.Random;


public class EnemyMovement : MonoBehaviour
{
    public GameManager gameManager;

    public VasePush vasePush;
    Transform vaseTrans;

    public NavMeshAgent agent;
    public float range; //radius of sphere  

    public Transform centrePoint; //centre of the area the agent wants to move around in  
      
    public Transform player;
    public float fieldOfView = 120f;

    public float distance;
    private bool playerInSight;
    private Vector3 lastRaycastDirection;
    private bool isLooking;
    public bool isVaseBroken = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        playerInSight = false;
    }

    void Update()
    {
        distance = Vector3.Distance(this.transform.position, player.position);

        CheckPlayerInSight();

        if (playerInSight)
        {
            chase();
        }
        else
        {
            RandomMove(isVaseBroken);
        }
    }

    private void chase()
    {
        agent.SetDestination(player.position);
        if (distance <= 6)
        {
            gameManager.Lose();
        }
        if (!playerInSight)
        {
            RandomMove(isVaseBroken);
        }
    }

    private void CheckPlayerInSight()
    {
        Vector3 direction = player.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);

        if (angle < fieldOfView * 0.5f)
        {
            RaycastHit hit;
            isLooking = Physics.Raycast(transform.position + Vector3.up * 1.5f, direction.normalized, out hit, range);
            if (isLooking == player)
            {
                lastRaycastDirection = direction.normalized;
                if (hit.transform == player)
                {
                    playerInSight = true;
                    Debug.Log("Player in sight");
                    return;
                }
            }
        }

        playerInSight = false;
    }

    private void OnDrawGizmos()
    {
        if (playerInSight)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }

        Gizmos.DrawRay(transform.position + Vector3.up * 1.5f, lastRaycastDirection * range);
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randdom = UnityEngine.Random.insideUnitSphere;


        Vector3 randomPoint = center + randdom * range; //random point in a sphere  
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 5.0f, NavMesh.AllAreas))
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big  
            //or add a for loop like in the documentation  
            result = hit.position;
            return true;
        }

        Debug.LogWarning($"Failed to find a valid NavMesh position for random point");
        result = Vector3.zero;
        return false;
    }

    private void RandomMove(bool isVaseBroken)
    {
        if (agent.remainingDistance <= agent.stoppingDistance && !isVaseBroken) //done with path  
        {
            Vector3 point;
            if (RandomPoint(centrePoint.position, range, out point)) //pass in our centre point and radius of area  
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos  
                agent.SetDestination(point);
                Debug.Log($"New Destination Set: {point}");
            }
        }
    }

    public void moveToVase(Transform vaseTrans)
    {
        
        agent.SetDestination(vaseTrans.position);
    }

    public void Caught()
    {
        

    }
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySight : MonoBehaviour
{
    public float fieldOfViewAngle = 110f;           // Number of degrees, centred on forward, for the enemy see.
    public bool playerInSight;                      // Whether or not the player is currently sighted.
  //  public Vector3 personalLastSighting;            // Last place this enemy spotted the player.
    public GameObject targetHero;
    public bool onlyTargetNPC;

    private UnityEngine.AI.NavMeshAgent nav;                       // Reference to the NavMeshAgent component.
    private SphereCollider col;                     // Reference to the sphere collider trigger component.
    //private LastPlayerSighting lastPlayerSighting;  // Reference to last global sighting of the player.
    private List<GameObject> players;                      // Reference to the player.
    private Animator playerAnim;                    // Reference to the player's animator component.
   // private PlayerHealth playerHealth;              // Reference to the player's health script.
    //private HashIDs hash;                           // Reference to the HashIDs.
    private Vector3 previousSighting;               // Where the player was sighted last frame.
   


    void Start()
    {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        col = GetComponent<SphereCollider>();
        players = new List<GameObject>();
        if (!onlyTargetNPC)
        {
            players.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
        }
        players.AddRange(GameObject.FindGameObjectsWithTag("NPCHero"));
    }

    void OnTriggerStay(Collider other)
    {
        if(targetHero == null)
        {
            foreach (var hero in players)
            {
                if (other.gameObject == hero)
                {
                    Vector3 direction = other.transform.position - transform.position;
                    float angle = Vector3.Angle(direction, transform.forward);
                    if (angle < fieldOfViewAngle * 0.5f)
                    {
                        RaycastHit hit;
                        if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, col.radius))
                        {
                            if (hit.collider.gameObject == hero)
                            {
                                targetHero = hero;
                                playerInSight = true;
                            }
                        }
                    }

                    if (CalculatePathLength(hero.transform.position) <= col.radius)
                    {
                        // personalLastSighting = hero.transform.position;
                        targetHero = hero;
                        playerInSight = true;
                    }
                }
            }
        }
    }

    float CalculatePathLength(Vector3 targetPosition)
    {
        // Create a path and set it based on a target position.
        UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
        if (nav.enabled)
            nav.CalculatePath(targetPosition, path);

        // Create an array of points which is the length of the number of corners in the path + 2.
        Vector3[] allWayPoints = new Vector3[path.corners.Length + 2];

        // The first point is the enemy's position.
        allWayPoints[0] = transform.position;

        // The last point is the target position.
        allWayPoints[allWayPoints.Length - 1] = targetPosition;

        // The points inbetween are the corners of the path.
        for (int i = 0; i < path.corners.Length; i++)
        {
            allWayPoints[i + 1] = path.corners[i];
        }

        // Create a float to store the path length that is by default 0.
        float pathLength = 0;

        // Increment the path length by an amount equal to the distance between each waypoint and the next.
        for (int i = 0; i < allWayPoints.Length - 1; i++)
        {
            pathLength += Vector3.Distance(allWayPoints[i], allWayPoints[i + 1]);
        }

        return pathLength;
    }
}
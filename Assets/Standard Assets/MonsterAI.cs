using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class MonsterAI : MonoBehaviour
    {
        //public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        //public Transform target; // target to aim for
        private Animator animator;
        private EnemySight enemySight;
        // Reference to the EnemySight script.
        private NavMeshAgent nav;                               // Reference to the nav mesh agent.
        //private Transform player;                               // Reference to the player's transform.
       // private LastPlayerSighting lastPlayerSighting;          // Reference to the last global sighting of the player.
        private float chaseTimer;                               // A timer for the chaseWaitTime.
        private float patrolTimer;                              // A timer for the patrolWaitTime.
        private int wayPointIndex;                              // A counter for the way point array.
        private bool isEscaping;

        public float patrolSpeed = 2f;                          // The nav mesh agent's speed when patrolling.
        public float chaseSpeed = 5f;                           // The nav mesh agent's speed when chasing.
        public float chaseWaitTime = 5f;                        // The amount of time to wait when the last sighting is reached.
        public float patrolWaitTime = 1f;                       // The amount of time to wait when the patrol way point is reached.
        private Transform[] patrolWayPoints;                     // An array of transforms for the patrol route.
        public Transform patrolWayPoint1;
        public Transform patrolWayPoint2;
        public bool onlyTargetNPC;

        // Use this for initialization
        private void Start()
        {
            enemySight = GetComponent<EnemySight>();
            enemySight.onlyTargetNPC = onlyTargetNPC;
            nav = GetComponent<NavMeshAgent>();
            //player = GameObject.FindGameObjectWithTag("Hero").transform;
            animator = GetComponent<Animator>();
            animator.Play("creature1walkforward");
            // get the components on the object we need ( should not be null due to require component so no need to check )
            nav = GetComponentInChildren<NavMeshAgent>();
            //character = GetComponent<ThirdPersonCharacter>();
            nav.updateRotation = true;
            nav.updatePosition = true;
            patrolWayPoints = new [] { patrolWayPoint1, patrolWayPoint2 };
            nav.destination = patrolWayPoints[wayPointIndex].position;
            //int[] n3 = { 2, 4, 6, 8 };
        }

        void Chasing()
        {
            nav.destination = enemySight.targetHero.transform.position;
            if (Vector3.Distance(nav.nextPosition, nav.destination) > 2.0f)
            {
                animator.SetTrigger("creature1run");
                nav.speed = chaseSpeed;
                AudioSource audio = GetComponent<AudioSource>();
                audio.Play();
               
            } else
            {
                animator.SetTrigger("creature1attack1");
                if(enemySight.targetHero.tag.Equals("NPCHero"))
                {
                    var aiControl = enemySight.targetHero.GetComponent<AICharacterControl>();
                    if (aiControl != null)
                    {
                        aiControl.characterState.Injure(aiControl);
                    }
                }
                
                if(enemySight.targetHero.tag.Equals("Hero"))
                {
                    var player = enemySight.targetHero.GetComponent<FirstPerson.FirstPersonController>();
                    if (player != null)
                    {
                        player.Kill();
                    }
                }
                nav.speed = 0;
            }


            
            // If the the last personal sighting of the player is not close...
            //if (sightingDeltaPos.sqrMagnitude > 4f)
            //    // ... set the destination for the NavMeshAgent to the last personal sighting of the player.
            //    nav.destination = enemySight.personalLastSighting;

            // Set the appropriate speed for the NavMeshAgent.
            //nav.speed = chaseSpeed;

            //// If near the last personal sighting...
            //if (nav.remainingDistance < nav.stoppingDistance)
            //{
            //    // ... increment the timer.
            //    chaseTimer += Time.deltaTime;

            //    // If the timer exceeds the wait time...
            //    if (chaseTimer >= chaseWaitTime)
            //    {      
            //        //enemySight.personalLastSighting = lastPlayerSighting.resetPosition;
            //        chaseTimer = 0f;
            //    }
            //}
            //else
            //    // If not near the last sighting personal sighting of the player, reset the timer.
            //    chaseTimer = 0f;
        }

        void Patrolling()
        {
            nav.speed = patrolSpeed;
            //if (Vector3.Distance(destination, target.position) > 1.0f)
            //{
            //    destination = target.position;
            //    agent.destination = destination;
            //}
            if(Vector3.Distance(nav.nextPosition, nav.destination) < 1.0f)
            {
                // ... increment the wayPointIndex.
                if (wayPointIndex == patrolWayPoints.Length - 1)
                    wayPointIndex = 0;
                else
                    wayPointIndex++;
            }
             nav.destination = patrolWayPoints[wayPointIndex].position;
        }
    
        // Update is called once per frame
        private void Update()
        {
            if(isEscaping)
            {
                Escape();
            } else {
                // If the player is in sight and is alive...
                if (enemySight.playerInSight)
                    Chasing();
                else
                    Patrolling();
            }
        }

        private void Escape()
        {
            nav.speed = chaseSpeed;
            wayPointIndex = 0;
            nav.destination = patrolWayPoints[wayPointIndex].position;
            if (Vector3.Distance(nav.nextPosition, nav.destination) < 1.0f)
            {
                enemySight.targetHero = null;
                enemySight.playerInSight = false;
                isEscaping = false; //go back to patrolling
            }
        }


        public void HitByFlashlight()
        {
            isEscaping = true;
 
        }
    }
}

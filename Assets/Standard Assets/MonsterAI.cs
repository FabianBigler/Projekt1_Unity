using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class MonsterAI : MonoBehaviour
    {
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target; // target to aim for
        private Animator animator;

        private EnemySight enemySight;                          // Reference to the EnemySight script.
        private NavMeshAgent nav;                               // Reference to the nav mesh agent.
        private Transform player;                               // Reference to the player's transform.
       // private LastPlayerSighting lastPlayerSighting;          // Reference to the last global sighting of the player.
        private float chaseTimer;                               // A timer for the chaseWaitTime.
        private float patrolTimer;                              // A timer for the patrolWaitTime.
        private int wayPointIndex;                              // A counter for the way point array.

        public float patrolSpeed = 2f;                          // The nav mesh agent's speed when patrolling.
        public float chaseSpeed = 5f;                           // The nav mesh agent's speed when chasing.
        public float chaseWaitTime = 5f;                        // The amount of time to wait when the last sighting is reached.
        public float patrolWaitTime = 1f;                       // The amount of time to wait when the patrol way point is reached.
        public Transform[] patrolWayPoints;                     // An array of transforms for the patrol route.

        void Awake()
        {
            // Setting up the references.
            //enemySight = GetComponent<EnemySight>();
            nav = GetComponent<NavMeshAgent>();
            player = GameObject.FindGameObjectWithTag("Hero").transform;
            //lastPlayerSighting = GameObject.FindGameObjectWithTag(Tags.gameController).GetComponent<LastPlayerSighting>();
        }

        // Use this for initialization
        private void Start()
        {
            animator = GetComponent<Animator>();
            //animator.SetTrigger("creature1run");
            //animator.SetTrigger("creature1walkforward");
            animator.Play("creature1walkforward");
            // get the components on the object we need ( should not be null due to require component so no need to check )
            nav = GetComponentInChildren<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
          //  moveState = MoveState.Follow;
            nav.updateRotation = false;
            nav.updatePosition = true;;
        }

        void Chasing()
        {

        }


        // Update is called once per frame
        private void Update()
        {
            //switch (moveState)
            //{
            //    case MoveState.Follow:
            //        if (target != null)
            //        {
            //            agent.SetDestination(target.position);
            //            character.Move(agent.desiredVelocity, false, false);
            //        }
            //        else
            //        {
            //            character.Move(Vector3.zero, false, false);
            //        }
            //        break;
            //    case MoveState.Stand:
            //        agent.SetDestination(Vector3.zero);
            //        character.Move(Vector3.zero, false, false);
            //        break;
            //}
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}

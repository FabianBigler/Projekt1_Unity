using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class MonsterAI : MonoBehaviour
    {
        public NavMeshAgent agent { get; private set; } // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target; // target to aim for
        public MoveState moveState;
        public AICharacterState characterState;
        private Animator animator;

        // Use this for initialization
        private void Start()
        {
            animator = GetComponent<Animator>();
            animator.Play("creature1run");
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
            moveState = MoveState.Follow;
            agent.updateRotation = false;
            agent.updatePosition = true;;
        }


        // Update is called once per frame
        private void Update()
        {
            switch (moveState)
            {
                case MoveState.Follow:
                    if (target != null)
                    {
                        agent.SetDestination(target.position);
                        character.Move(agent.desiredVelocity, false, false);
                    }
                    else
                    {
                        character.Move(Vector3.zero, false, false);
                    }
                    break;
                case MoveState.Stand:
                    agent.SetDestination(Vector3.zero);
                    character.Move(Vector3.zero, false, false);
                    break;
            }
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}

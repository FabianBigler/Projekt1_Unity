using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public NavMeshAgent agent { get; private set; } // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
        public Transform target; // target to aim for
        public MoveState moveState;
        public AICharacterState characterState;

        public bool IsInjured;
        public bool IsDead;
        public bool IsScared;

        // Use this for initialization
        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
            characterState = MoveState.Follow;

            agent.updateRotation = false;
	        agent.updatePosition = true;
        }

        public void SetState(AICharacterState newState)
        {
            characterState = newState;
            //characterState.Blind
        }


        // Update is called once per frame
        private void Update()
        {
            if (moveState == MoveState.Follow)
            {
                if (target != null)
                {
                    agent.SetDestination(target.position);
                    // use the values to move the character
                    character.Move(agent.desiredVelocity, false, false);
                }
                else
                {
                    // We still need to call the character's move function, but we send zeroed input as the move param.
                    character.Move(Vector3.zero, false, false);
                }
            }
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }

    public enum MoveState
    {
        Follow,
        Stand
    }
}

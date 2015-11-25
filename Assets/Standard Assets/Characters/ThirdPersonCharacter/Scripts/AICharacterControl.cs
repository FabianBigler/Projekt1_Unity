using System;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (NavMeshAgent))]
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        public NavMeshAgent agent { get; private set; } // the navmesh agent required for the path finding
        public ThirdPersonCharacter character { get; private set; } // the character we are controlling
       // public Transform target; // target to aim for
        public MoveState moveState;
        public GameObject player;
        public AICharacterState characterState;
        public bool IsInjured { get; set; }
        public bool IsBlinded { get; set; }
        public bool IsScared { get; set; }
        public bool IsDead { get; set; }
        private List<string> defaultAnswers;
        private List<string> scaredAnswers;
        private List<string> deadAnswers;
        private List<string> blindedAnswers;

        public FirstPerson.FirstPersonController playerController { get; private set; }

        // Use this for initialization
        private void Start()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
            moveState = MoveState.Follow;
            SetState(new AICharacterStateFollow());
            agent.updateRotation = false;
	        agent.updatePosition = true;

            defaultAnswers = new List<string>();
            defaultAnswers.Add("Hey, we need to hurry! Kelly is in danger!");
            defaultAnswers.Add("Don't you dare thinking about touching me, we got more pressing matters to attend to!");
            defaultAnswers.Add("What's up?");
            defaultAnswers.Add("What are you looking at?");
            scaredAnswers = new List<string>();
            scaredAnswers.Add("Thanks for talking to me.");
            scaredAnswers.Add("OK, blast it. Let's find our daughter!");
            deadAnswers = new List<string>();
            deadAnswers.Add("You failed me!");
            deadAnswers.Add("Go away!");
            blindedAnswers = new List<string>();
            blindedAnswers.Add("That's not funny!");
            blindedAnswers.Add("You're pissing me off. Lead the way!");
            blindedAnswers.Add("Will you stop that now?");
            playerController = player.GetComponent<FirstPerson.FirstPersonController>();    
        }

        public void SetState(AICharacterState newState)
        {
            characterState = newState;                        
        }

        // Update is called once per frame
        private void Update()
        {
            switch(moveState)
            {
                case MoveState.Follow:
                    if (player != null)
                    {
                        agent.SetDestination(player.transform.position);
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

        public string GetAnswer()
        {
            string answer = string.Empty;
            if (IsScared)
            {
                answer = getRandomAnswer(scaredAnswers);
            }
            if (IsDead)
            {
                answer = getRandomAnswer(deadAnswers);
            }
            if (IsBlinded)
            {
                answer = getRandomAnswer(blindedAnswers);
            }
            if (string.IsNullOrEmpty(answer))
            {
                answer = getRandomAnswer(defaultAnswers);
            }
            return answer;
        }

        private string getRandomAnswer(List<string> answers)
        {
            UnityEngine.Random rand = new UnityEngine.Random();
            int randIndex = UnityEngine.Random.Range(0, (answers.Count - 1));
            return answers[randIndex];
        }

        //public void SetTarget(Transform target)
        //{
        //    this.target = target;
        //}

        void OnTriggerStay(Collider other)
        {
            var player = other.gameObject.GetComponent<FirstPerson.FirstPersonController>();
            if (player != null)
            {
                moveState = MoveState.Stand;
            }
        }

        void OnTriggerExit(Collider other)
        {
            var player = other.gameObject.GetComponent<FirstPerson.FirstPersonController>();
            if (player != null)
            {
                if(!IsScared)
                {
                    moveState = MoveState.Follow;
                }
            }
        }
    }

    public enum MoveState
    {
        Follow,
        Stand
    }
}

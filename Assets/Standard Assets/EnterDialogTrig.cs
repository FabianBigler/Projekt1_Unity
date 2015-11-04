using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Collections.Generic;

public class EnterDialogTrig : MonoBehaviour {
    //private List<string> answers;

    //void Start()
    //{
    //    var answers = new System.Collections.Generic.List<string>();
    //    answers.Add("Hey, we need to hurry! Kelly is in danger!");
    //    answers.Add("Don't you dare thinking about touching me, we got more pressing matters to attend to!");
    //    answers.Add("What's up?");
    //    answers.Add("What are you looking at?");
    //}

    //public GameObject hero;
    void OnTriggerEnter(Collider other)
    {
        var controller = other.gameObject.GetComponent<FirstPersonController>();
        controller.Tell("Jenny", getRandomAnswer());
        var control = GetComponent<AICharacterControl>();
        //control.state = MoveState.Stand;
    }

    private string getRandomAnswer()
    {
        var answers = new List<string>();
        answers.Add("Hey, we need to hurry! Kelly is in danger!");
        answers.Add("Don't you dare thinking about touching me, we got more pressing matters to attend to!");
        answers.Add("What's up?");
        answers.Add("What are you looking at?");
        Random rand = new Random();
        int randIndex = Random.Range(0, (answers.Count - 1));
        return answers[randIndex];
    }
}

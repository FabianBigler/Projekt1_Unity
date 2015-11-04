using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Collections.Generic;

public class EnterDialogTrig : MonoBehaviour {    
    private List<string> defaultAnswers;
    private List<string> scaredAnswers;
    private List<string> deadAnswers;
    private List<string> blindedAnswers;

    void Start()
    {
        defaultAnswers = new List<string>();
        defaultAnswers.Add("Hey, we need to hurry! Kelly is in danger!");
        defaultAnswers.Add("Don't you dare thinking about touching me, we got more pressing matters to attend to!");
        defaultAnswers.Add("What's up?");
        defaultAnswers.Add("What are you looking at?");
        scaredAnswers = new List<string>();
        scaredAnswers.Add("I'm so scared!");
        scaredAnswers.Add("Please don't leave me alone!");
        deadAnswers = new List<string>();
        deadAnswers.Add("You failed me!");
        deadAnswers.Add("Go away!");
        blindedAnswers = new List<string>();
        blindedAnswers.Add("That's not funny!");
        blindedAnswers.Add("You're pissing me off. Lead the way!");
        blindedAnswers.Add("Will you stop that now?");
    }

    //public GameObject hero;
    void OnTriggerEnter(Collider other)
    {                
        var controller = other.gameObject.GetComponent<FirstPersonController>();
        var control = GetComponent<AICharacterControl>();

        string answer = string.Empty;
        if(control.IsScared)
        {
            answer = getRandomAnswer(scaredAnswers);
        }
        if(control.IsDead)
        {
            answer = getRandomAnswer(deadAnswers);
        }
        if(control.IsBlinded)
        {
            answer = getRandomAnswer(blindedAnswers);
        }

        if(string.IsNullOrEmpty(answer))
        {
            answer = getRandomAnswer(defaultAnswers);
        }

        controller.Tell("Jenny", answer);
    }

    private string getRandomAnswer(List<string> answers)
    {     
        Random rand = new Random();
        int randIndex = Random.Range(0, (answers.Count - 1));
        return defaultAnswers[randIndex];
    }
}






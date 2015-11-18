using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Collections.Generic;

public class EnterDialogTrig : MonoBehaviour {    
    private AICharacterControl characterControl;
    private bool keyIsPressed;

    void Start()
    {
        characterControl = GetComponent<AICharacterControl>();
    }

    //public GameObject hero;
    void OnTriggerStay(Collider other)
    {               
        var player = other.gameObject.GetComponent<FirstPersonController>();
        if(player != null)
        {
            if (Input.GetKeyDown("e"))
            {
                var control = GetComponent<AICharacterControl>();
                player.Tell("Jenny", control.GetAnswer());
                control.characterState.TalkTo(control);
            }
        }
    }
}






using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Characters.ThirdPerson;
using System.Collections.Generic;

public class EnterDialogTrig : MonoBehaviour {    
    private AICharacterControl characterControl;

    void Start()
    {
        characterControl = GetComponent<AICharacterControl>();
    }

    //public GameObject hero;
    void OnTriggerEnter(Collider other)
    {               
        var player = other.gameObject.GetComponent<FirstPersonController>();
        if(player != null)
        {
            var control = GetComponent<AICharacterControl>();
            player.Tell("Jenny", control.GetAnswer());
            control.characterState.TalkTo(control);
        }
    }
}






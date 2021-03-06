﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class EnterHouseTrig : MonoBehaviour {
    public GameObject hero;
    public string nextLevel = "L1_HauntedHouse";
    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Hero"))
        {
            var controller = other.gameObject.GetComponent<FirstPersonController>();
            var item = (controller.inventory != null ? controller.inventory.GetItem("Key") : null);
            if (item == null)
            {
                controller.LoadQuest(2);
            }
            else
            {
                Application.LoadLevel(nextLevel);
                controller.LoadQuest(4);
            }
        }
    }
   
}

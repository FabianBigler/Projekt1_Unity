using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.FirstPerson;

public class EnterHouseTrig : MonoBehaviour {
    public GameObject hero;
    void OnTriggerEnter(Collider other)
    {
        var controller = other.gameObject.GetComponent<FirstPersonController>();
        var item = controller.inventory.GetItem("Key");
        if (item == null)
        {
            controller.LoadQuest(2);
        }
        else
        {
            controller.LoadQuest(4);
        }
    }
   
}

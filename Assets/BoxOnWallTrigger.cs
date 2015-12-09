using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoxOnWallTrigger : MonoBehaviour
{
    public GameObject KeyToActivate;

    private bool successTriggered;

    void Start()
    {
        successTriggered = false;
    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("element with name <<" + other.gameObject.name + ">> entered triggerCollider");
        if (other.gameObject.name.Equals("targetBox"))
        {
            CheckSuccess();
        }
    }

    private void CheckSuccess()
    {
            if (!successTriggered)
            {
                successTriggered = true;
                Debug.Log("Mission completed!");
                KeyToActivate.SetActive(true);
            }
        }

}

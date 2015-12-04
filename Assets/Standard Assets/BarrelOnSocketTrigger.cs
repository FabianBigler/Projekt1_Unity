using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BarrelOnSocketTrigger : MonoBehaviour
{
    public GameObject KeyToActivate;

    static List<string> barrelSockets;
    private bool successTriggered;

    void Start()
    {
        barrelSockets = new List<string>();
        successTriggered = false;
    }
    void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("raisableObject"))
        {
            if (!barrelSockets.Contains(tag)) barrelSockets.Add(tag);
            CheckSuccess();
        }
    }

    private void CheckSuccess()
    {
        if(barrelSockets.Count == 2)
        {
            if(!successTriggered)
            {
                successTriggered = true;
                Debug.Log("Mission completed!");
                KeyToActivate.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("raisableObject"))
        {
            barrelSockets.Remove(tag);
        }
    }
}

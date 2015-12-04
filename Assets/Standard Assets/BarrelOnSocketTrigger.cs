using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BarrelOnSocketTrigger : MonoBehaviour
{
    public GameObject KeyToActivate;

    static List<string> barrelSockets;
    void Start()
    {
        barrelSockets = new List<string>();
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
            Debug.Log("Mission completed!");
            KeyToActivate.SetActive(true);
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

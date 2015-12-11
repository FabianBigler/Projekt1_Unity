using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LightWallTrigger : MonoBehaviour
{
    public GameObject KeyToActivate;
    public int Order;
    public Light light;

    static List<String> lightwalls;
    private bool successTriggered;
   


    void Start()
    {
        lightwalls = new List<String>();
        successTriggered = false;
        //light = GetComponentInChildren<Light>();
        
        //if (light != null) Debug.Log("FOUND LIGHT!");
    }

    public void Update()
    {
        if(lightwalls.Contains(tag))
        {
            light.intensity = 3;
            light.enabled = true;
            Debug.Log(tag + ": Light ON!");
        } else
        {
            light.intensity = 0;
        }
    }

    public void HitByFlashLight()
    {
        if (lightwalls.Contains(tag)) return;
        if(Order == lightwalls.Count + 1)
        {
            lightwalls.Add(tag);
            Debug.Log("Added Lightwall");
            if (lightwalls.Count == 3)
            {
                if (!successTriggered)
                {
                    successTriggered = true;
                    Debug.Log("Mission completed!");
                    KeyToActivate.SetActive(true);
                }
            }
        } else
        {
            lightwalls.Clear();
        }
    }
}

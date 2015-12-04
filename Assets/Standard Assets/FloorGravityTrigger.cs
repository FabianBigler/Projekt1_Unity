﻿using UnityEngine;
using System.Collections;

public class FloorGravityTrigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered!");
        if(other.CompareTag("Hero"))
        {
            Debug.Log("It should fall down now *cough*");
            GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
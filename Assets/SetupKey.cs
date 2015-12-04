using UnityEngine;
using System.Collections;

public class SetupKey : MonoBehaviour {
    public Color MaterialColor;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Renderer>().material.color = MaterialColor;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

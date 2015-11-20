using UnityEngine;
using System.Collections;

public class ItemRotator : MonoBehaviour {
    public double rotationsPerMinute = 10.0;
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(0, (float)(6.0 * rotationsPerMinute * Time.deltaTime), 0);
    }
}

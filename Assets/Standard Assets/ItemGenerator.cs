using UnityEngine;
using System.Collections;


public class ItemGenerator : MonoBehaviour {
    public float minPos = 0;
    public float maxPos = 2000;
    public int countItems = 10000;
    public GameObject gameObject;

	// Use this for initialization
	void Start () {

        for (var i = 0; i < countItems; i++)
        {
             var theNewPos= new Vector3(Random.Range(minPos,maxPos),0.1f,Random.Range(minPos,maxPos));
             var obj = Instantiate(gameObject);
             obj.transform.position = theNewPos;
         }
	}
}

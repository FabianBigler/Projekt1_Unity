using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RayCastRaisableObject : MonoBehaviour
{
	//INSPECTOR SETTINGS
	public float Reach = 5F; // Within this radius the player is able to open/close the door.

	//PRIVATE SETTINGS
	[HideInInspector] public bool InReach;

	//DEBUGGING
	public Color DebugRayColor = Color.red;
    public string GameTagCollider;
    private GameObject hitObject;

    //START FUNCTION
    void Start()
	{

	}

	//UPDATE FUNCTION
	void Update()
	{
		Ray ray = Camera.main.ViewportPointToRay (new Vector3 (0.5F, 0.5F, 0F));

        if (Input.GetMouseButtonDown(0))
		{
            RaycastHit hit;
                
            if (Physics.Raycast(ray, out hit, Reach) && hit.collider.tag == GameTagCollider)
            {
                Debug.Log("raisableObject hit");
                hitObject = hit.collider.gameObject;
                hitObject.transform.parent = gameObject.transform;

                var p = hitObject.transform.position;
                hitObject.GetComponent<Rigidbody>().useGravity = false;
                hitObject.transform.position = new Vector3(p.x, p.y + 1, p.z);
               
                //hitObject.transform.position.Set(gameObject.transform.position.x, gameObject.transform.position.y + 10, gameObject.transform.position.z);
                //gameObject.transform.Translate(Vector3.up * Time.deltaTime, Space.World);
            }

        }
        if (Input.GetMouseButtonUp(0)) // This will release the object 
        {
            if(hitObject!= null) hitObject.GetComponent<Rigidbody>().useGravity = true;
            hitObject.transform.parent = null;
            hitObject = null;

            //var p = hitObject.transform.position;
            //hitObject.transform.position = new Vector3(p.x, p.y - 1, p.z);
        }

	}
}
